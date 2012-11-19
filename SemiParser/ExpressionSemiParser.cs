using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SemiParser
{
    public class ExpressionSemiParser : Parser<Variable[]>
    {
        public bool Verify(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return false;

            var exp = expression.Prepare();
            return CheckInteger(exp) && TryDisassemble(exp);
        }

        public Variable[] Parse(string expression)
        {
            var exp = expression.ReplaceSpace();
            var expPrepared = exp.Prepare();
            return MatchVariables(expPrepared, exp.Length != expPrepared.Length);
        }

        private Variable[] MatchVariables(string expression, bool shouldShift)
        {
            var variables = new List<Variable>();
            Regex rex = new Regex(@"(?<=\(|\|)[A-z]\w{0,}(?=\)|\|)");
            var results = rex.Matches(expression);
            foreach (Match match in results)
            {
                var offset = shouldShift ? match.Index - 1 : match.Index;
                variables.Add(new Variable(offset, match.Length, expression.Substring(match.Index, match.Length)));
            }
            return variables.ToArray();
        }

        private bool CheckInteger(string expression)
        {
            Regex rex = new Regex(@"(?<=\(|\|)[0-9.]+(?=\)|\|)");
            var results = rex.Matches(expression);
            foreach (Match match in results)
            {
                int oth;
                if (!Int32.TryParse(match.Value, out oth))
                    return false;
            }
            return true;
        }

        private bool TryDisassemble(string expression)
        {
            var textForReplace = "xxx";
            var exp = expression.ReplaceLastParameters(")")
                .ReplaceFunctions(textForReplace)
                .ReplaceConstants(textForReplace)
                .ReplaceWrapExpression(textForReplace)
                .ReplaceVariables(string.Empty)
                .ReplaceWrap(string.Empty)
                .RemoveBrackets();
            return exp.Length == 0;
        }
    }
}
