using GrammarToParseTable.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Grammer
{
    class Follow
    {
        //public static Dictionary<Rule, HashSet<Symbol>> FindAllFollows(List<Rule> rules, Dictionary<Rule, HashSet<Symbol>> firsts)
        //{
        //    Dictionary<Rule, HashSet<Symbol>> follows = new Dictionary<Rule, HashSet<Symbol>>();
        //    foreach (Rule r in rules)
        //    {
        //        follows[r] = new HashSet<Symbol>();
        //    }

        //    // add $ in 1st rule's follow (+ any rule with the same character as the 1st rule):
        //    foreach (Rule r in rules)
        //    {
        //        if (r.left.character == rules[0].left.character)
        //        {
        //            follows[r].Add(new Symbol('$'));
        //        }
        //    }

        //    // create a new follow:
        //    Dictionary<Rule, HashSet<Symbol>> current_follows = new Dictionary<Rule, HashSet<Symbol>>();
        //    foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in follows)
        //    {
        //        current_follows[entry.Key] = new HashSet<Symbol>(entry.Value);
        //    }

        //    // update the follows until nothing changes:
        //    bool follows_changed = true;
        //    while (follows_changed)
        //    {

        //        foreach (Rule current_rule in rules)
        //        {
        //            char current_char = current_rule.left.character;
        //            List<Rule> found_rules = new List<Rule>();

        //            // search right side of every rule for the current left side:
        //            foreach (Rule r in rules)
        //            {
        //                if (r.left.character == current_rule.left.character)
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    foreach (List<Symbol> lst in r.right)
        //                    {
        //                        foreach (Symbol s in lst)
        //                        {
        //                            if (s.character == current_char)
        //                            {
        //                                found_rules.Add(r);
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            // do stuff to the found ones (containing the Nonterminal):             
        //            foreach (Rule r in found_rules)
        //            {
        //                foreach (List<Symbol> lst in r.right)
        //                {
        //                    for (int i = 0; i < lst.Count(); ++i)
        //                    {
        //                        if (lst.ElementAt(i).character == current_char)
        //                        {
        //                            if (i + 1 < lst.Count())
        //                            {
        //                                Symbol next_sym = lst.ElementAt(i + 1); // the next element
        //                                if (next_sym is Terminal)
        //                                {
        //                                    current_follows[current_rule].Add(next_sym);
        //                                }
        //                                else if (next_sym is Nonterminal)
        //                                {
        //                                    // get the first of it:
        //                                    HashSet<Symbol> fi = new HashSet<Symbol>();
        //                                    foreach (Rule rr in firsts.Keys)
        //                                    {
        //                                        if (rr.left.character == next_sym.character)
        //                                        {
        //                                            fi.UnionWith(firsts[rr]);
        //                                        }
        //                                    }

        //                                    // if epsilon exists in the first:
        //                                    if (fi.Contains(new Symbol('ε')))
        //                                    {
        //                                        // union with firsts without epsilon + follow of that rule:
        //                                        fi.Remove(new Symbol('ε'));
        //                                        current_follows[current_rule].UnionWith(fi);
        //                                        foreach (Rule r_ in rules)
        //                                        {
        //                                            if (r_.left.character == next_sym.character)
        //                                            {
        //                                                current_follows[current_rule].UnionWith(current_follows[r_]);
        //                                            }
        //                                        }
        //                                    }
        //                                    else // if no epsilon:
        //                                    {
        //                                        current_follows[current_rule].UnionWith(fi);
        //                                    }

        //                                }
        //                                else
        //                                {
        //                                    throw new Exception("Error finding Follow - Neither Terminal nor Nonterminal? (Why?!)");
        //                                }
        //                            }
        //                            else // it is the right most symbol:
        //                            {
        //                                foreach (Symbol s in current_follows[r]) 
        //                                {
        //                                    current_follows[current_rule].Add(s);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //        }

        //        // check if follows has changed:
        //        follows_changed = false;
        //        foreach (var r in follows.Keys)
        //        {
        //            foreach (Symbol sym in current_follows[r])
        //            {
        //                if (!follows[r].Contains(sym))
        //                {
        //                    follows_changed = true;
        //                    goto End;
        //                }
        //            }
        //        }
        //        follows_changed = false;
        //        End:

        //        // copy the new follows to main follow follows:
        //        if (follows_changed)
        //        {
        //            for (int i = 0; i < follows.Keys.Count(); ++i) 
        //            {
        //                var r = follows.Keys.ElementAt(i);
        //                follows[r] = new HashSet<Symbol>(current_follows[r]);
        //            }
        //        }

        //    }
        //    return follows;
        //}

        /// <summary>
        /// Generates the Dict of Follows
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static Dictionary<Nonterminal, HashSet<Terminal>> getFollowDic(List<Rule> rules)
        {
            Dictionary<Nonterminal, HashSet<Terminal>> followsDic = new Dictionary<Nonterminal, HashSet<Terminal>>();

            bool isStartingSymbol = true;
            for (int i = 0; i < 3; i++)
                foreach (Rule rule in rules)
                {
                    // Number of nonterminals are lesthan or eaqual to rule numbers
                    try
                    {
                        followsDic.Add(rule.left, getFollow(rule.left, rules, isStartingSymbol));
                        isStartingSymbol = false;
                    }
                    catch (Exception)
                    {

                    }
                }

            return followsDic;
        }

        public static HashSet<Terminal> getFollow(Nonterminal symbol, List<Rule> rules, bool isStartingSymbol = false)
        {
            HashSet<Terminal> follows = new HashSet<Terminal>();

            if (isStartingSymbol)
                follows.Add(new Terminal('$'));


            foreach (Rule rule in rules)
            {
                if (rule.right[0].Contains(symbol))
                {
                    // production has symbol in its right side
                    if (rule.right[0].IndexOf(symbol) == rule.right[0].Count - 1)
                    {
                        // this symbol is the last symbol
                        follows.UnionWith(getFollow(rule.left, rules));
                    }
                    /// Must change to chek if its the last symbol
                    if (rule.right[0].IndexOf(symbol) < rule.right[0].Count)
                    {
                        // there is a symbol after this symbol
                        Symbol right = rule.right[0][rule.right[0].IndexOf(symbol) + 1];

                        if (right is Nonterminal)
                        {
                            HashSet<Terminal> first = First.getFirst(right, rules);
                            // the symbol after this symbol is nonterminal
                            if (first.Contains(new Terminal('ε')))
                            {
                                // and it has epsilon in its Firsts
                                first.Remove(new Terminal('ε'));
                                follows.UnionWith(first);
                            }
                        }
                        else
                        {
                            // the symbol after this symbol is terminal
                            follows.Add(new Terminal(symbol.character));
                        }
                    }
                }
            }

            return follows;
        }
    }
}
