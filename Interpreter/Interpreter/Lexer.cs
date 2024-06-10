using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Lexer
    {
        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();

            // Expressoes regulares para os tokens
            string numberPattern = @"[0-9]+(?:\.[0-9]+)?";
            string variablePattern = @"[a-zA-Z]+";
            string operatorPattern = @"[\+\-\*/]";
            string leftParenthesisPattern = @"\(";
            string rightParenthesisPattern = @"\)";

            string pattern = $"{numberPattern}|{variablePattern}|{operatorPattern}|{leftParenthesisPattern}|{rightParenthesisPattern}";

            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                if (Regex.IsMatch(match.Value, numberPattern))
                {
                    tokens.Add(new Token(TokenType.Number, match.Value));
                }
                else if (Regex.IsMatch(match.Value, variablePattern))
                {
                    tokens.Add(new Token(TokenType.Variable, match.Value));
                }
                else if (Regex.IsMatch(match.Value, operatorPattern))
                {
                    tokens.Add(new Token(TokenType.Operator, match.Value));
                }
                else if (Regex.IsMatch(match.Value, leftParenthesisPattern))
                {
                    tokens.Add(new Token(TokenType.LeftParenthesis, match.Value));
                }
                else if (Regex.IsMatch(match.Value, rightParenthesisPattern))
                {
                    tokens.Add(new Token(TokenType.RightParenthesis, match.Value));
                }
            }
            return tokens;
        }
    }

    public enum TokenType
    {
        Number,
        Variable,
        Operator,
        LeftParenthesis,
        RightParenthesis
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

}
