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

        private static List<Rule> visited_rules = new List<Rule>();

        public static HashSet<Symbol> FindFirst(List<Rule> rules, Rule rule)
        {
            HashSet<Symbol> result = new HashSet<Symbol>();
            if (rule.right.Count == 0)
            {
                throw new Exception("Input Error - Empty right side!");
            }
            else if (rule.right[0].Count == 0)
            {
                result.Add(null);
            }
            Symbol first_rSymbol = rule.right[0][0];
            if (first_rSymbol is Terminal)
            {
                result.Add(first_rSymbol);
            }
            else if (first_rSymbol is Nonterminal)
            {
                if (visited_rules.Contains(rule)) // It's in a loop!
                {
                    throw new Exception("Error - Nonterminals in a loop.");
                }
                visited_rules.Add(rule);
                // find the next target rules:
                List<Rule> next_rules = new List<Rule>();
                foreach (Rule r in rules)
                {
                    if (r.left.character == first_rSymbol.character && !visited_rules.Contains(r))
                    {
                        next_rules.Add(r);   
                    }
                }
                // get results for each:
                foreach (Rule r in next_rules)
                {
                    HashSet<Symbol> subresults = FindFirst(rules, r);
                    result.UnionWith(subresults);
                }              
            }
            else
            {
                throw new Exception("Error - unexpected First");
            }

            visited_rules.Clear();
            return result;
        }

    
    }

}
