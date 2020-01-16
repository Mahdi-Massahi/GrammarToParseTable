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

        private Dictionary<Rule, Symbol> firsts;
        private Dictionary<Rule, Symbol> follows = new Dictionary<Rule, Symbol>();

        public ParseTable(List<Rule> rules)
        {
            firsts = new Dictionary<Rule, Symbol>();
            follows = new Dictionary<Rule, Symbol>();
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
