
using System;
using System.Collections.Generic;
using System.IO;

namespace Syntax
{
    class Parser
    {
        // Estrutura que armazena a tabela preditiva LL(1)
        private Dictionary<string, Dictionary<string, string>> parsingTable;

        public Parser(string csvFilePath)
        {
            parsingTable = new Dictionary<string, Dictionary<string, string>>();
            LoadParsingTable(csvFilePath);
        }

        // Inicia a tabela de um arquivo CSV
        private void LoadParsingTable(string csvFilePath)
        {
            var lines = File.ReadAllLines(csvFilePath);
            var headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i].Split(',');
                var nonTerminal = row[0];
                parsingTable[nonTerminal] = new Dictionary<string, string>();

                for (int j = 1; j < headers.Length; j++)
                {
                    var terminal = headers[j];
                    var production = row[j].Trim();

                    if (!string.IsNullOrEmpty(production))
                    {
                        parsingTable[nonTerminal][terminal] = production;
                    }
                }
            }
        }

        // Função para analisar a entrada com base na tabela
        public bool Parse(string input)
        {
            var stack = new Stack<string>();
            stack.Push("$"); // símbolo de fim de entrada
            stack.Push("E"); // símbolo inicial

            var tokens = new Queue<string>(input.Split(' '));
            tokens.Enqueue("$"); 

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var currentToken = tokens.Peek();

                if (IsTerminal(top))
                {
                    if (top == currentToken)
                    {
                        tokens.Dequeue(); // Desempilhar
                    }
                    else
                    {
                        Console.WriteLine($"Erro: Esperado '{top}', mas foi encontrado '{currentToken}'.");
                        return false;
                    }
                }
                else
                {
                    if (parsingTable.ContainsKey(top) && parsingTable[top].ContainsKey(currentToken))
                    {
                        var production = parsingTable[top][currentToken];
                        if (production != "ε")
                        {
                            var symbols = production.Split(' ');
                            for (int i = symbols.Length - 1; i >= 0; i--)
                            {
                                stack.Push(symbols[i]);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Erro: Nenhuma produção encontrada para '{top}' com lookahead '{currentToken}'.");
                        return false;
                    }
                }
            }

            return tokens.Count == 0;
        }

        // Verifica se um símbolo é terminal
        private bool IsTerminal(string symbol)
        {
            return !parsingTable.ContainsKey(symbol);
        }

        static void Main(string[] args)
        {
            var parser = new Parser("parse_table.csv");

            Console.WriteLine("Digite a cadeia de entrada (separando os tokens por espaços):");
            var input = Console.ReadLine();

            if (parser.Parse(input))
            {
                Console.WriteLine("Entrada aceita!");
            }
            else
            {
                Console.WriteLine("Entrada rejeitada!");
            }
        }
    }

}
