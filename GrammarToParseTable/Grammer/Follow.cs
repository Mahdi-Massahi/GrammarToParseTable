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
            Dictionary<Rule, HashSet<Symbol>> result = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (Rule r in rules)
            {
                result[r] = new HashSet<Symbol>();
            }

            // add $ in 1st rule's follow (+ any rule with the same character as the 1st rule):
            foreach (Rule r in rules)
            {
                if (r.left.character == rules[0].left.character)
                {
                    result[r].Add(new Symbol('$'));
                }
            }

            // create a new follow result:
            Dictionary<Rule, HashSet<Symbol>> current_res = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in result)
            {
                current_res[entry.Key] = new HashSet<Symbol>(entry.Value);
            }

            // update the follow result until nothing changes:
            bool follows_changed = true;
            while (follows_changed)
            {

                // do stuff to the current_result:
                //
                // TODO
                //

                // check if result has changed:
                follows_changed = false;
                foreach (var r in result.Keys)
                {
                    foreach (Symbol sym in current_res[r])
                    {
                        if (!result[r].Contains(sym))
                        {
                            follows_changed = true;
                            goto End;
                        }
                    }
                }
                End:

                // copy the new result to main follow result:
                if (follows_changed)
                {
                    foreach (var r in result.Keys)
                    {
                        result[r] = new HashSet<Symbol>(current_res[r]);
                    }
                }

            }
            return result;
        }
        
    }
}
