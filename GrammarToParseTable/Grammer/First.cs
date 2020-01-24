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
        /// <summary>
        /// Generates the Dict of Firsts
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
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
        /// Return an specific symbol's first in a rule list.
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
