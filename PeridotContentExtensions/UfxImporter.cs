using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Text;
using System.Text.RegularExpressions;

namespace PeridotContentExtensions
{
    [ContentImporter(".ufx", DefaultProcessor = "Effect - MonoGame", DisplayName = "UFX Importer")]
    public class UfxImporter : ContentImporter<EffectContent>
    {
        public override EffectContent Import(string filename, ContentImporterContext context)
        {
            EffectContent effect = new()
            {
                Identity = new ContentIdentity(filename)
            };

            using StreamReader reader = new(filename);
            string code = reader.ReadToEnd();

            List<string> lines = code.Split("\n").Select(x => x.TrimEnd()).ToList();

            List<UberBlock> uberBlocks = new();
            List<UberTechnique> uberTechniques = new();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (!line.TrimStart().StartsWith("$")) continue;

                string cmd = line.TrimStart().Substring(1).Replace(", ", ",");

                if (cmd.TrimStart().StartsWith("uberblock "))
                {
                    List<string> uberBlockLines = new();

                    // remove lines up to $endblock
                    while (line.TrimStart() != "$endblock")
                    {
                        uberBlockLines.Add(line);
                        lines.RemoveAt(i);
                        line = lines[i];
                    }

                    // remove $endblock line
                    lines.RemoveAt(i);

                    string blockName = cmd.Substring("uberblock ".Length).Split("(")[0].Trim();

                    string[] blockArgs = cmd.Split("(")[1][..^1].Replace(" ", "").Split(",");

                    uberBlocks.Add(new UberBlock() { Name = blockName, Args = blockArgs, Lines = uberBlockLines.ToArray() });
                }
                else if (cmd.TrimStart().StartsWith("ubertechnique "))
                {
                    // remove $ubertechnique line
                    lines.RemoveAt(i--);

                    UberTechnique uberTechnique = new();

                    string[] techniqueArgs = cmd.Substring("ubertechnique ".Length).Split(" ");

                    if (techniqueArgs.Length != 1 && techniqueArgs.Length != 2)
                        throw new ArgumentException("$ubertechnique command requires 1 or 2 arguments (vertex and pixel shader).");

                    if (techniqueArgs.Length == 1 && !techniqueArgs[0].Contains("("))
                        throw new ArgumentException(
                            "If $ubertechnique uses one argument that argument needs to be a uberblock invocation.");

                    if (techniqueArgs[0].Contains("("))
                        uberTechnique.VertexShader = UberBlockInvocation.Parse(techniqueArgs[0]);
                    else
                        uberTechnique.VertexShader = techniqueArgs[0];

                    if (techniqueArgs.Length == 2)
                    {
                        if (techniqueArgs[1].Contains("("))
                            uberTechnique.PixelShader = UberBlockInvocation.Parse(techniqueArgs[1]);
                        else
                            uberTechnique.PixelShader = techniqueArgs[1];
                    }
                    else
                    {
                        uberTechnique.PixelShader = uberTechnique.VertexShader;
                    }

                    uberTechniques.Add(uberTechnique);
                }
            }

            // build the .fx file
            StringBuilder sb = new();
            foreach (string line in lines)
            {
                Regex regex = new(@"^#include ""(.*)""$");

                Match match = regex.Match(line);

                if (!match.Success)
                {
                    sb.AppendLine(line);
                    continue;
                }

                string includePath = match.Groups[1].Value;
                includePath = Path.Combine(Path.GetDirectoryName(filename), includePath);

                sb.Append(File.ReadAllText(includePath));
            }

            HashSet<UberBlockInvocation> uberBlockInvocations = new();
            foreach (UberTechnique uberTechnique in uberTechniques)
            {
                if (uberTechnique.VertexShader is UberBlockInvocation invoc)
                {
                    uberBlockInvocations.Add(invoc);
                }

                if (uberTechnique.PixelShader is UberBlockInvocation invoc2)
                {
                    uberBlockInvocations.Add(invoc2);
                }
            }

            foreach (UberBlockInvocation uberInvoc in uberBlockInvocations)
            {
                UberBlock uberBlock = uberBlocks.First(x => x.Name == uberInvoc.Name);

                Dictionary<string, bool> argValues = new();
                if (uberBlock.Args.Length != uberInvoc.ArgValues.Length)
                    throw new Exception("Number of provided arguments for the uberblock invocation does not match expected number.");
                for (int i = 0; i < uberBlock.Args.Length; i++)
                {
                    string argName = uberBlock.Args[i];
                    bool argValue = uberInvoc.ArgValues[i];

                    argValues.Add(argName, argValue);
                }

                for (int i = 0; i < uberBlock.Lines.Length; i++)
                {
                    string line = uberBlock.Lines[i];
                    line = line.Replace("$$", uberInvoc.Identification);

                    if (!line.Trim().StartsWith("$"))
                    {
                        sb.AppendLine(line);
                        continue;
                    }

                    if (line.Trim().StartsWith("$if"))
                    {
                        string expr = line.Split("$if")[1];
                        foreach ((string argName, bool argValue) in argValues.OrderByDescending(x => x.Key.Length))
                        {
                            expr = expr.Replace(argName, argValue.ToString());
                        }
                        bool result = BoolExpressionEvaluator.Evaluate(expr);

                        int ifsFound = 1;

                        // skip until #endif
                        if (!result)
                        {
                            while (ifsFound > 0)
                            {
                                i++;

                                line = uberBlock.Lines[i];

                                if (line.Trim().StartsWith("$if")) ifsFound++;
                                else if (line.Trim().StartsWith("$endif")) ifsFound--;
                            }
                        }
                    }
                }
            }

            // write technique definitions
            foreach (UberTechnique technique in uberTechniques)
            {
                sb.AppendLine("technique");
                sb.AppendLine("{");
                sb.AppendLine("pass P0");
                sb.AppendLine("{");


                sb.Append("VertexShader = compile VS_SHADERMODEL ");
                if (technique.VertexShader is UberBlockInvocation invocV)
                {
                    sb.Append("VertexShader");
                    sb.Append(invocV.Identification);
                }
                else
                {
                    sb.Append(technique.VertexShader);
                }
                sb.AppendLine("();");


                sb.Append("PixelShader = compile PS_SHADERMODEL ");
                if (technique.PixelShader is UberBlockInvocation invocP)
                {
                    sb.Append("PixelShader");
                    sb.Append(invocP.Identification);
                }
                else
                {
                    sb.Append(technique.PixelShader);
                }
                sb.AppendLine("();");


                sb.AppendLine("}");
                sb.AppendLine("};");
                sb.AppendLine();
            }

            string tmpFile = Path.GetTempFileName();

            string resultCode = sb.ToString();

            File.WriteAllText(tmpFile, resultCode);

            effect.EffectCode = resultCode;
            effect.Identity = new ContentIdentity(tmpFile);
            return effect;
        }

        private class UberBlock
        {
            public string Name;
            public string[] Args;
            public string[] Lines;
        }

        private class UberBlockInvocation
        {
            public readonly string Name;
            public readonly bool[] ArgValues;

            public UberBlockInvocation(string name, bool[] argValues)
            {
                Name = name;
                ArgValues = argValues;
            }

            public string Identification => "_" + Name + "_" + string.Join("_", ArgValues);

            protected bool Equals(UberBlockInvocation other)
            {
                return Name == other.Name && ArgValues.Equals(other.ArgValues);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((UberBlockInvocation)obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, ArgValues);
            }

            public static UberBlockInvocation Parse(string input)
            {
                UberBlockInvocation result = new(
                    input.Split("(")[0],
                    input.Split("(")[1][..^1].Split(",").Select(x => bool.Parse(x.Trim())).ToArray());

                return result;
            }
        }

        private class UberTechnique
        {
            public object VertexShader;
            public object PixelShader;
        }
    }
}