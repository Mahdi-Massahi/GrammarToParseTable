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
        public static Dictionary<Rule, HashSet<Symbol>> FindAllFollows(List<Rule> rules, Dictionary<Rule, HashSet<Symbol>> firsts)
        {
            Dictionary<Rule, HashSet<Symbol>> follows = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (Rule r in rules)
            {
                follows[r] = new HashSet<Symbol>();
            }

            // add $ in 1st rule's follow (+ any rule with the same character as the 1st rule):
            foreach (Rule r in rules)
            {
                if (r.left.character == rules[0].left.character)
                {
                    follows[r].Add(new Symbol('$'));
                }
            }

            // create a new follow:
            Dictionary<Rule, HashSet<Symbol>> current_follows = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in follows)
            {
                current_follows[entry.Key] = new HashSet<Symbol>(entry.Value);
            }

            // update the follows until nothing changes:
            bool follows_changed = true;
            while (follows_changed)
            {
                
                foreach (Rule current_rule in rules)
                {
                    char current_char = current_rule.left.character;
                    List<Rule> found_rules = new List<Rule>();

                    // search right side of every rule for the current left side:
                    foreach (Rule r in rules)
                    {
                        if (r.left.character == current_rule.left.character)
                        {
                            continue;
                        }
                        else
                        {
                            foreach (List<Symbol> lst in r.right)
                            {
                                foreach (Symbol s in lst)
                                {
                                    if (s.character == current_char)
                                    {
                                        found_rules.Add(r);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // do stuff to the found ones (containing the Nonterminal):             
                    foreach (Rule r in found_rules)
                    {
                        foreach (List<Symbol> lst in r.right)
                        {
                            for (int i = 0; i < lst.Count(); ++i)
                            {
                                if (lst.ElementAt(i).character == current_char)
                                {
                                    if (i + 1 < lst.Count())
                                    {
                                        Symbol next_sym = lst.ElementAt(i + 1); // the next element
                                        if (next_sym is Terminal)
                                        {
                                            current_follows[current_rule].Add(next_sym);
                                        }
                                        else if (next_sym is Nonterminal)
                                        {
                                            // get the first of it:
                                            HashSet<Symbol> fi = new HashSet<Symbol>();
                                            foreach (Rule rr in firsts.Keys)
                                            {
                                                if (rr.left.character == next_sym.character)
                                                {
                                                    fi.UnionWith(firsts[rr]);
                                                }
                                            }

                                            // if epsilon exists in the first:
                                            if (fi.Contains(new Symbol('ε')))
                                            {
                                                // union with firsts without epsilon + follow of that rule:
                                                fi.Remove(new Symbol('ε'));
                                                current_follows[current_rule].UnionWith(fi);
                                                foreach (Rule r_ in rules)
                                                {
                                                    if (r_.left.character == next_sym.character)
                                                    {
                                                        current_follows[current_rule].UnionWith(current_follows[r_]);
                                                    }
                                                }
                                            }
                                            else // if no epsilon:
                                            {
                                                current_follows[current_rule].UnionWith(fi);
                                            }

                                        }
                                        else
                                        {
                                            throw new Exception("Error finding Follow - Neither Terminal nor Nonterminal? (Why?!)");
                                        }
                                    }
                                    else // it is the right most symbol:
                                    {
                                        foreach (Symbol s in current_follows[r]) 
                                        {
                                            current_follows[current_rule].Add(s);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                // check if follows has changed:
                follows_changed = false;
                foreach (var r in follows.Keys)
                {
                    foreach (Symbol sym in current_follows[r])
                    {
                        if (!follows[r].Contains(sym))
                        {
                            follows_changed = true;
                            goto End;
                        }
                    }
                }
                follows_changed = false;
                End:

                // copy the new follows to main follow follows:
                if (follows_changed)
                {
                    for (int i = 0; i < follows.Keys.Count(); ++i) 
                    {
                        var r = follows.Keys.ElementAt(i);
                        follows[r] = new HashSet<Symbol>(current_follows[r]);
                    }
                }

            }
            return follows;
        }
        
    }
}
