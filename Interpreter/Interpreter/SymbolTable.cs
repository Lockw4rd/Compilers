using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class SymbolTable
    {
        private Dictionary<string, double> variables;

        public SymbolTable()
        {
            variables = new Dictionary<string, double>();
        }

        // Metodo para adicionar ou atualizar a tabela de simbolos
        public void SetVariable(string name, double value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
            }
            else
            {
                variables.Add(name, value);
            }
        }

        // Metodo para recuperar variavel da tabela de simbolos
        public double GetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }
            else
            {
                throw new Exception($"Variable '{name}' is not defined.");
            }
        }

        // Metodo para verificar se uma variavel pertence a tabela de simbolos
        public bool IsVariableDefined(string name)
        {
            return variables.ContainsKey(name);
        }
    }
}
