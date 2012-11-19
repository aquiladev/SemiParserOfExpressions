using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SemiParser
{
    public static class Extender
    {
        public static string ReplaceVariables(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(?<=\(|\|)(\s+|)[A-z]\w{0,}(\s+|)(\|)");
            var result = rex.Replace(expression, textForReplace);
            if (rex.IsMatch(result))
            {
                result = result.ReplaceVariables(textForReplace);
            }
            return result;
        }

        public static string ReplaceWrapExpression(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(?<=\(|\|)(\s+|)(\()(\s+|)[A-z]\w{0,}(\s+|)(\))(\s+|)(?=\)|\|)");
            var result = rex.Replace(expression, textForReplace);
            if (rex.IsMatch(result))
            {
                result = result.ReplaceWrapExpression(textForReplace);
            }
            return result;
        }

        public static string ReplaceWrap(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(\()(\s+|)[A-z]\w{0,}(\s+|)(\))");
            var result = rex.Replace(expression, textForReplace);
            return result;
        }

        public static string ReplaceConstants(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(?<=\(|\|)(\s+|)[0-9.]+(\s+|)(?=\)|\|)");
            var result = rex.Replace(expression, textForReplace);
            if (rex.IsMatch(result))
            {
                result = result.ReplaceConstants(textForReplace);
            }
            return result;
        }

        public static string ReplaceFunctions(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(\s+|)[A-z]\w{0,}\((\s+|)([A-z]\w{0,}|[0-9]+|\""[A-z0-9 ]+\"")(\s+|)\)(\s+|)(?=\)|\|)");
            var result = rex.Replace(expression, textForReplace);
            if (rex.IsMatch(result))
            {
                result = result.ReplaceFunctions(textForReplace);
            }
            return result;
        }

        public static string ReplaceLastParameters(this string expression, string textForReplace)
        {
            Regex rex = new Regex(@"(\|)(\s+|)([A-z]\w+|[0-9]+|\""[A-z0-9 ]+\"")(\s+|)(\))");
            var result = rex.Replace(expression, textForReplace);
            if (rex.IsMatch(result))
            {
                result = result.ReplaceLastParameters(textForReplace);
            }
            return result;
        }

        public static string ReplaceArithmeticActs(this string expression)
        {
            return expression
                .Replace('+', '|')
                .Replace('-', '|')
                .Replace('*', '|')
                .Replace('/', '|')
                .Replace(',', '|');
        }

        public static string ReplaceSpace(this string expression)
        {
            return expression.Replace(" ", string.Empty);
        }

        public static string AddBrackets(this string expression)
        {
            if (expression.Length > 0
                && (expression[0] != '(' || expression[expression.Length - 1] != ')'))
            {
                expression = string.Format("({0})", expression);
            }
            return expression;
        }

        public static string Prepare(this string expression)
        {
            return expression
                .AddBrackets()
                .ReplaceArithmeticActs();
        }

        public static string RemoveBrackets(this string expression)
        {
            Regex rex = new Regex(@"(\()(\s+|)(\))");
            var result = rex.Replace(expression, string.Empty);
            if (rex.IsMatch(result))
            {
                result = result.RemoveBrackets();
            }
            return result;
        }
    }
}
