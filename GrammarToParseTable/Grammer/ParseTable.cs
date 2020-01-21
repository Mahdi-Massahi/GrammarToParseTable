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
        public Dictionary<char, HashSet<Symbol>> res_first { get; private set; }
        public Dictionary<char, HashSet<Symbol>> res_follow { get; private set; }

        /// <summary>
        /// Returns a list of first and follow items to be used in datagrid
        /// </summary>
        /// <returns>A list of FiFoItems</returns>
        public List<FiFoItem> getFiFoItems()
        {
            List<FiFoItem> data = new List<FiFoItem>();
            for (int i = 0; i < res_first.Count; i++)
            {
                char r = res_first.ElementAt(i).Key;
                data.Add(new FiFoItem()
                {
                    Number = i+1,
                    Production = r.ToString(),
                    First = SymbolHashSetToString(res_first[r]),
                    Follow = SymbolHashSetToString(res_follow[r])
                });
            }
            return data;
        }

        /// <summary>
        /// This class is only for UI element: Datagrid - nothing else
        /// </summary>
        public class FiFoItem
        {
            public int Number { get; internal set;}
            public string Production { get; internal set;}
            public string First { get; internal set;}
            public string Follow { get; internal set; }
        }

        /// <summary>
        /// Converts the HashSet<Symbol> set to String in bakets
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private String SymbolHashSetToString(HashSet<Symbol> set)
        {
            String data = "{ ";

            foreach (Symbol symbol in set)
                data += symbol.character.ToString() + ", ";
            if (set.Count == 0)
                data += " ";

            data = data.Substring(0, data.Length-2) + " }";
            return data;
        }


        public List<Terminal> getTerminals(List<Rule> rules)
        {
            List<Terminal> nonterminals = new List<Terminal>();
            foreach (Rule r in rules)
            {
                nonterminals.Add(new Terminal(r.left.character));
            }
            return nonterminals;
        }

    }
}
