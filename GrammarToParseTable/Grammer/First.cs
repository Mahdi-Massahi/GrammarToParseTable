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

            HashSet<Symbol> firsts = new HashSet<Symbol>();
            
            foreach (List<Symbol> lst in rule.right)
            {
                for (int i = 0; i < lst.Count; ++i)
                {
                    Symbol sym = lst.ElementAt(i);
                    if (sym is Terminal)
                    {
                        firsts.Add(sym);
                        break;
                    }
                    else if (sym is Nonterminal)
                    {
                        // for every rule with sym in the left side:                        
                        HashSet<Symbol> sub_firsts = new HashSet<Symbol>();
                        foreach (Rule r in rules)
                        {
                            if (r.left.character == sym.character)
                            {
                                sub_firsts.UnionWith(FindFirst(rules, r));
                            }
                        }
                        if (sub_firsts.Contains(new Symbol('ε')))
                        {
                            while (i + 1 < lst.Count)
                            {
                                if (sub_firsts.Contains(new Symbol('ε')))
                                {
                                    sub_firsts.Remove(new Symbol('ε')); // omit the epsilon if there exist other symbols further
                                    i++;
                                    Symbol next_sym = lst.ElementAt(i);
                                    if (next_sym is Terminal)
                                    {
                                        sub_firsts.Add(next_sym);
                                        break; // break the while loop if a terminal was found
                                    }
                                    else if (next_sym is Nonterminal)
                                    {
                                        foreach (Rule r in rules)
                                        {
                                            if (r.left.character == next_sym.character)
                                            {
                                                sub_firsts.UnionWith(FindFirst(rules, r));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Error - Neither Terminal nor Nonterminal in finding first");
                                    }
                                }
                            }

                        }
                        firsts.UnionWith(sub_firsts);
                    }
                    else
                    {
                        throw new Exception("Error - Neither Terminal nor Nonterminal in finding first");
                    }
                }
            }


            return firsts;

        }

    }
}
