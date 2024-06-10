using System;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer();
            SymbolTable symbolTable = new SymbolTable();
            Parser parser = new Parser(lexer, symbolTable);
            
            Console.WriteLine("Interpreter"); Console.WriteLine(" ");
            string? input = "";

            do
            {
                Console.Write(">");
                input = Console.ReadLine();
                try
                {
                    // Expressao atribuida ao Parser
                    string output = parser.Parse(input);

                    // Escreve a expressao pos-fixada
                    Console.Write("Expressao Pos-fixada: ");
                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }

            } while (!String.IsNullOrEmpty(input));
            
        }

        // Exemplos:

        // Input: "2 + 2 + 3"
        // Output: "2 2 + 3 +"

        // Input: "9 - 5 + 2"
        // Output: "9 5 - 2 +"

        // Input: "3 * (a + b) / 2"
        // Output: "3 a b + * 2 /"

    }

}
