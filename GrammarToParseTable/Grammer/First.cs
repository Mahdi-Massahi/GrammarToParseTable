using GrammarToParseTable.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Grammer
{
    class First
    {

        private static List<Symbol> visited_nonterminals = new List<Symbol>();

        public static Symbol FindFirst(List<Rule> rules, Rule rule)
        {
            Symbol symbol;
            if (rule.right.Count == 0)
            {
                throw new Exception("Syntax Error - Empty right side!");
            }
            else if (rule.right[0].Count == 0)
            {
                symbol = null;
            }
            if (rule.right[0][0] is Terminal)
            {
                symbol = rule.right[0][0];
            }
            else if (rule.right[0][0] is Nonterminal)
            {
                if (visited_nonterminals.Contains(rule.right[0][0])) // It's in a loop!
                {
                    throw new Exception("Error - Nonterminals in a loop! Fix the grammar.");
                }
                visited_nonterminals.Add(rule.right[0][0]);
                symbol = FindFirst(rules, rule);
            }
            else
            {
                throw new Exception("Error - unexpected First");
            }

            visited_nonterminals.Clear();
            return symbol;
        }

    
    }

}
