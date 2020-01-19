using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrammarToParseTable.Classes;

namespace GrammarToParseTable.Grammer
{
    class ParseTable
    {

        public Dictionary<Rule, HashSet<Symbol>> firsts { get; private set; }
        public Dictionary<Rule, HashSet<Symbol>> follows { get; private set; }

        public ParseTable(List<Rule> rules)
        {
            firsts = new Dictionary<Rule, HashSet<Symbol>>();
            follows = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (Rule r in rules)
            {
                firsts[r] = First.FindFirst(rules, r);
            }
            foreach (Rule r in rules)
            {
                follows[r] = Follow.FindFollow(rules, firsts, r);
            }
        }

        

    }
}
