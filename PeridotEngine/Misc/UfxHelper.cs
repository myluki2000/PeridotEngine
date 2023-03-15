using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Misc
{
    public static class UfxHelper
    {
        public static string GenerateTechniqueId(string combinationsId, params object[] args)
        {
            StringBuilder sb = new(combinationsId);
            sb.Append('_');

            IEnumerable<string> argsString = args.Select(x =>
            {
                if (x is bool argBool)
                {
                    return argBool ? "1" : "0";
                }

                throw new ArgumentException("Argument type not allowed by UFX. Argument Type was " + x.GetType());
            });

            sb.Append(string.Join("_", argsString));
            
            return sb.ToString();
        }
    }
}
