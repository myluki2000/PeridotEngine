using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeridotContentExtensions
{
    internal class BoolExpressionEvaluator
    {
        public static bool Evaluate(string expression)
        {
            expression = expression.Replace(" ", "").Replace("||", "|").Replace("&&", "&");
            return EvalExpr(expression);
        }

        private static bool EvalExpr(string expr)
        {
            expr = expr.ToLower();

            expr = expr.Replace("(true)", "true").Replace("(false)", "false");
            expr = expr.Replace("!true", "false").Replace("!false", "true");

            if (expr == "true") return true;
            if (expr == "false") return false;

            // check for the occurence of the "literal & literal" expression. We can instantly
            // evaluate it and replace its occurence in the input string with a literal.
            Regex regex = new(@"(true|false)&(true|false)");
            while (true)
            {
                Match match = regex.Match(expr);

                // keep searching expr for the "literal & literal" expression until we can't find
                // one anymore
                if (!match.Success) break;

                string[] s = match.Value.Split("&");
                // the new literal to replace the expression
                string newS = (EvalExpr(s[0]) && EvalExpr(s[1])).ToString();

                expr = expr.Substring(0, match.Index) + newS + expr.Substring(match.Index + match.Length);
            }

            // check for the occurence of the "literal | literal" expression and evaluate it.
            regex = new(@"(true|false)\|(true|false)");
            while (true)
            {
                Match match = regex.Match(expr);

                // keep searching expr for the "literal | literal" expression until we can't find
                // one anymore
                if (!match.Success) break;

                string[] s = match.Value.Split("|");
                // the new literal to replace the expression
                string newS = (EvalExpr(s[0]) || EvalExpr(s[1])).ToString();

                expr = expr.Substring(0, match.Index) + newS + expr.Substring(match.Index + match.Length);
            }

            return EvalExpr(expr);
        }
    }
}
