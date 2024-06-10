using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Parser
    {
        private Lexer lexer;
        private SymbolTable symbolTable;

        public Parser(Lexer lexer, SymbolTable symbolTable)
        {
            this.lexer = lexer;
            this.symbolTable = symbolTable;
        }

        public string Parse(string input)
        {
            // Transforma a expressao em tokens
            List<Token> tokens = lexer.Tokenize(input);

            // Nova string para a expressao pos-fixada
            string postfixExpression = "";

            // Nova pilha para os operadores
            Stack<Token> operatorStack = new Stack<Token>();

            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.Number || token.Type == TokenType.Variable)
                {
                    // Atribui numeros e variaveis para a expressao
                    postfixExpression += token.Value + " ";
                }
                else if (token.Type == TokenType.Operator)
                {
                    while (operatorStack.Count > 0 && 
                       operatorStack.Peek().Type == TokenType.Operator &&
                       Precedence(token) <= Precedence(operatorStack.Peek()))
                    {
                        postfixExpression += operatorStack.Pop().Value + " ";
                    }
                    operatorStack.Push(token);
                }
                else if (token.Type == TokenType.LeftParenthesis)
                {
                    operatorStack.Push(token);
                }
                else if (token.Type == TokenType.RightParenthesis)
                {
                    while (operatorStack.Peek().Type != TokenType.LeftParenthesis)
                    {
                        postfixExpression += operatorStack.Pop().Value + " ";
                    }
                    // Descarta o parentese esquerdo
                    operatorStack.Pop();
                }
            }

            // Atribui os operadores restantes para a expressao
            while (operatorStack.Count > 0)
            {
                postfixExpression += operatorStack.Pop().Value + " ";
            }

            return postfixExpression.Trim();
        }

        // Funcao para definir precedencia
        private int Precedence(Token token)
        {
            switch (token.Value)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
