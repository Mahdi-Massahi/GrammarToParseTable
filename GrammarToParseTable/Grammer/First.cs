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
        public static HashSet<Symbol> FindFirst(List<Rule> rules, Rule rule)
        {

            HashSet<Symbol> firsts = new HashSet<Symbol>();

            //foreach (List<Symbol> lst in rule.right)
            //{
            //    for (int i = 0; i < lst.Count; ++i)
            //    {
            //        Symbol sym = lst.ElementAt(i);
            //        if (sym is Terminal)
            //        {
            //            firsts.Add(sym);
            //            break;
            //        }
            //        else if (sym is Nonterminal)
            //        {
            //            // for every rule with sym in the left side:                        
            //            HashSet<Symbol> sub_firsts = new HashSet<Symbol>();
            //            foreach (Rule r in rules)
            //            {
            //                if (r.left.character == sym.character)
            //                {
            //                    sub_firsts.UnionWith(FindFirst(rules, r));
            //                }
            //            }
            //            if (sub_firsts.Contains(new Symbol('ε')))
            //            {
            //                while (i + 1 < lst.Count)
            //                {
            //                    if (sub_firsts.Contains(new Symbol('ε')))
            //                    {
            //                        sub_firsts.Remove(new Symbol('ε')); // omit the epsilon if there exist other symbols further
            //                        i++;
            //                        Symbol next_sym = lst.ElementAt(i);
            //                        if (next_sym is Terminal)
            //                        {
            //                            sub_firsts.Add(next_sym);
            //                            break; // break the while loop if a terminal was found
            //                        }
            //                        else if (next_sym is Nonterminal)
            //                        {
            //                            foreach (Rule r in rules)
            //                            {
            //                                if (r.left.character == next_sym.character)
            //                                {
            //                                    sub_firsts.UnionWith(FindFirst(rules, r));
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            throw new Exception("Error - Neither Terminal nor Nonterminal in finding first");
            //                        }
            //                    }
            //                }

            //            }
            //            firsts.UnionWith(sub_firsts);
            //        }
            //        else
            //        {
            //            throw new Exception("Error - Neither Terminal nor Nonterminal in finding first");
            //        }
            //    }
            //}


            return firsts;

        }

        public static Dictionary<Nonterminal, HashSet<Terminal>> getFirstDic(List<Rule> rules)
        {
            Dictionary<Nonterminal, HashSet<Terminal>> firstsDic = new Dictionary<Nonterminal, HashSet<Terminal>>();

            foreach (Rule rule in rules)
            {
                // Number of nonterminals are lesthan or eaqual to rule numbers
                try
                {
                    firstsDic.Add(rule.left, getFirst(rule.left, rules));
                }
                catch (Exception)
                {

                }
            }

            return firstsDic;
        }

        /// <summary>
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="rules">Must be symplifided</param>
        /// <returns></returns>
        public static HashSet<Terminal> getFirst(Symbol symbol, List<Rule> rules)
        {
            HashSet<Terminal> firsts = new HashSet<Terminal>();

            if (symbol is Terminal)
            {
                // Symbol is terminal
                firsts.Add(new Terminal(symbol.character));
                return firsts;
            }
            else
            {
                HashSet<Terminal> buff = new HashSet<Terminal>();

                // Symbol is nonterminal 
                // Look for the first in right side of the rule
                // Where the left side of it is symbol
                foreach (Rule rule in rules)
                {
                    // Check to find the right production
                    if (rule.left.Equals(symbol))
                    {
                        // Check if the fisrt symbol is epsilon itself or just a terminal
                        if (rule.right[0][0] is Terminal)
                            firsts.UnionWith(getFirst(rule.right[0][0], rules));
                        else
                        {
                            // First symbol in right side is a nonterminal
                            // Check all symbols one by one
                            for (int i = 0; i < rule.right[0].Count; i++)
                            {
                                buff = getFirst(rule.right[0][i], rules);

                                if (buff.Contains(new Terminal('ε')))
                                {
                                    // If was not the last symbol delete epsilon
                                    if (rule.right[0].Count - 1 != i)
                                        buff.Remove(new Terminal('ε'));
                                    firsts.UnionWith(buff);
                                }
                                else
                                {
                                    // The symbol does not contain epsilon
                                    firsts.UnionWith(buff);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return firsts;
        }
    }
}
