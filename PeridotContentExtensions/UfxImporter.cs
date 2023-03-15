using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Linq;
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

                    // parse uberblock line
                    Match match = Regex.Match(cmd, @"uberblock ([a-zA-Z0-9]*)\(([^()]*)\)( constraint ?\((.*)\))?$");

                    string blockName = match.Groups[1].Value;
                    string? argConstraint = match.Groups.Count >= 5 ? match.Groups[4].Value : null;

                    UberArg[] uberArgs = ParseUberArgsDefinition(match.Groups[2].Value);

                    uberBlocks.Add(new UberBlock() { Name = blockName, Args = uberArgs, Lines = uberBlockLines.ToArray(), ArgConstraint = argConstraint });
                }
                else if (cmd.TrimStart().StartsWith("ubertechnique "))
                {
                    // remove $ubertechnique line
                    lines.RemoveAt(i--);

                    UberTechnique uberTechnique = ParseUberTechniqueArgs(cmd.Substring("ubertechnique ".Length));

                    uberTechniques.Add(uberTechnique);
                }
                else if (cmd.TrimStart().StartsWith("ubercombinations "))
                {
                    // remove $ubercombinations line
                    lines.RemoveAt(i--);

                    // parse $ubercombinations line
                    Match match = Regex.Match(cmd, @"ubercombinations ([a-zA-Z0-9]+) ?\(([^()]*)\)( constraint ?\((.*)\))? technique (.*)$");

                    UberArg[] combinationArgs = ParseUberArgsDefinition(match.Groups[2].Value);
                    string constraintExpr = match.Groups[4].Value;
                    string combinationsIdentifier = match.Groups[1].Value;

                    foreach (IEnumerable<bool> combination in GetCombinations(new bool[] { false, true },
                                 combinationArgs.Length))
                    {
                        List<bool> combinationList = combination.ToList();
                        Dictionary<string, bool> argValues = Enumerable.Range(0, combinationArgs.Length)
                            .ToDictionary(i => combinationArgs[i].Name, i => combinationList[i]);

                        // sort arg dictionary by argument name length (decending), so that when replacing argument names with argument values in
                        // expressions we first replace the arguments with the longest name. This is to prevent wrong replacement if we for example
                        // have two arguments named "Foo" and "FooBar" (where one argument name is contained in another argument name)
                        argValues = argValues.OrderByDescending(x => x.Key.Length).ToDictionary(x => x.Key, x => x.Value);

                        // check if this combination of argument values is allowed by the uberblock's constraint (if there is any)
                        if (constraintExpr != "")
                        {
                            string expr = constraintExpr;
                            foreach ((string argName, bool argValue) in argValues)
                            {
                                expr = expr.Replace(argName, argValue.ToString());
                            }

                            if (!BoolExpressionEvaluator.Evaluate(expr))
                            {
                                // if this isn't a valid combination of arguments, continue to the next combination
                                continue;
                            }
                        }

                        // add uber technique with this set of arguments to the list of uber techniques
                        string uberTechniqueArgs = match.Groups[5].Value.Replace(", ", ",");

                        // replace argument names with argument values
                        foreach ((string argName, bool argValue) in argValues)
                        {
                            uberTechniqueArgs = Regex.Replace(uberTechniqueArgs, "[(,]" + argName + "[,)]", m =>
                            {
                                string s = m.Value;
                                return s[0] + argValue.ToString() + s[^1];
                            });
                        }

                        UberTechnique uberTechnique = ParseUberTechniqueArgs(uberTechniqueArgs);
                        uberTechnique.Identifier = combinationsIdentifier + "_" +
                                                   string.Join("_", combinationList.Select(x => x ? "1" : "0"));
                        uberTechniques.Add(uberTechnique);
                    }
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

            // put all uberblock invocations of the $ubertechnique calls into a hashset to remove duplicates
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

            // go through all uber invocations we need to write to the effect file
            foreach (UberBlockInvocation uberInvoc in uberBlockInvocations)
            {
                // find the uberblock corresponding to the uber invocation
                UberBlock uberBlock = uberBlocks.First(x => x.Name == uberInvoc.Name);

                Dictionary<string, bool> argValues = new();
                if (uberBlock.Args.Length != uberInvoc.ArgValues.Length)
                    throw new Exception("Number of provided arguments for the uberblock invocation does not match expected number.");
                for (int i = 0; i < uberBlock.Args.Length; i++)
                {
                    string argName = uberBlock.Args[i].Name;
                    bool argValue = uberInvoc.ArgValues[i];

                    argValues.Add(argName, argValue);
                }

                // sort arg dictionary by argument name length (decending), so that when replacing argument names with argument values in
                // expressions we first replace the arguments with the longest name. This is to prevent wrong replacement if we for example
                // have two arguments named "Foo" and "FooBar" (where one argument name is contained in another argument name)
                argValues = argValues.OrderByDescending(x => x.Key.Length).ToDictionary(x => x.Key, x => x.Value);

                // check if this combination of argument values is allowed by the uberblock's constraint (if there is any)
                if (uberBlock.ArgConstraint != "")
                {
                    string expr = uberBlock.ArgConstraint;
                    foreach ((string argName, bool argValue) in argValues)
                    {
                        expr = expr.Replace(argName, argValue.ToString());
                    }

                    if (!BoolExpressionEvaluator.Evaluate(expr))
                    {
                        throw new Exception("Uberblock invocation with argument values "
                                            + string.Join(", ", argValues.Select(x => x.Key + "=" + x.Value))
                                            + " is not allowed by uberblock's constraint");
                    }
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
                        foreach ((string argName, bool argValue) in argValues)
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
                if (!string.IsNullOrEmpty(technique.Identifier))
                {
                    sb.AppendLine("technique " + technique.Identifier);
                }
                else
                {
                    sb.AppendLine("technique");
                }
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

        private static UberTechnique ParseUberTechniqueArgs(string argsString)
        {
            UberTechnique uberTechnique = new();

            string[] techniqueArgs = argsString.Split(" ");

            if (techniqueArgs.Length != 1 && techniqueArgs.Length != 2)
                throw new ArgumentException("$ubertechnique command requires 1 or 2 arguments (vertex and pixel shader).");

            if (techniqueArgs.Length == 1 && !techniqueArgs[0].Contains("("))
                throw new ArgumentException(
                    "If $ubertechnique uses one argument that argument needs to be a uberblock invocation.");

            // first arg of the $ubertechnique is either a uber-invocation for a vertex shader or a regular vertex shader name
            if (techniqueArgs[0].Contains("("))
                uberTechnique.VertexShader = UberBlockInvocation.Parse(techniqueArgs[0]);
            else
                uberTechnique.VertexShader = techniqueArgs[0];

            // If there is a second argument, it is either a uber-invocation for a pixel shader or a regular pixel shader
            if (techniqueArgs.Length == 2)
            {
                if (techniqueArgs[1].Contains("("))
                    uberTechnique.PixelShader = UberBlockInvocation.Parse(techniqueArgs[1]);
                else
                    uberTechnique.PixelShader = techniqueArgs[1];
            }
            else
            {
                // if there is only one argument, then the uber-invocation is used for both the vertex and the pixel shader
                uberTechnique.PixelShader = uberTechnique.VertexShader;
            }

            return uberTechnique;
        }

        private static UberArg[] ParseUberArgsDefinition(string argsString)
        {
            List<UberArg> args = new();
            // parse args
            foreach (string s in argsString.Split(","))
            {
                string argString = s.Trim();
                string argTypeString = s.Split(" ")[0];
                string argName = s.Split(" ")[1];

                if (!Enum.TryParse(argTypeString, true, out UberArgType argType))
                {
                    throw new ArgumentException("$ubertechnique command argument of invalid type: " +
                                                argTypeString + " " + argName);
                }

                args.Add(new UberArg(argType, argName));
            }

            return args.ToArray();
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetCombinations(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private class UberBlock
        {
            public string Name;
            public UberArg[] Args;
            public string[] Lines;
            public string? ArgConstraint;
        }

        private class UberArg
        {
            public readonly UberArgType Type;
            public readonly string Name;

            public UberArg(UberArgType type, string name)
            {
                Type = type;
                Name = name;
            }
        }

        private enum UberArgType
        {
            BOOL,
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
            public string? Identifier = null;
            public object VertexShader;
            public object PixelShader;
        }
    }
}