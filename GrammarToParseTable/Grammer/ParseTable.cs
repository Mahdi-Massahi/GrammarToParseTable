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

        public ParseTable(List<Rule> rules)
        {
            Dictionary<Rule, HashSet<Symbol>>  firsts = new Dictionary<Rule, HashSet<Symbol>>();
            Dictionary<Rule, HashSet<Symbol>>  follows = new Dictionary<Rule, HashSet<Symbol>>();
            foreach (Rule r in rules)
            {
                firsts[r] = First.FindFirst(rules, r);
            }
            follows = Follow.FindAllFollows(rules, firsts);

            // quick fix for representing the duplicates:
            res_first = new Dictionary<char, HashSet<Symbol>>();
            foreach (Rule r in firsts.Keys)
            {
                if (res_first.ContainsKey(r.left.character))
                {
                    res_first[r.left.character].UnionWith(firsts[r]);
                }
                else
                {
                    res_first[r.left.character] = firsts[r];
                }
            }

            res_follow = new Dictionary<char, HashSet<Symbol>>();
            foreach (Rule r in follows.Keys)
            {
                if (res_follow.ContainsKey(r.left.character))
                {
                    res_follow[r.left.character].UnionWith(follows[r]);
                }
                else
                {
                    res_follow[r.left.character] = follows[r];
                }
            }
        }

        public void Print_Firsts()
        {
            Console.WriteLine("firsts:");
            foreach (KeyValuePair<char, HashSet<Symbol>> entry in res_first)
            {
                Console.Write(entry.Key + " : {");
                foreach (Symbol s in entry.Value)
                {
                    if (s.character == 'ε')
                    {
                        Console.Write("{0}", "Eps. ");
                    }
                    else
                    {
                        Console.Write("{0}", s.character);
                    }
                }
                Console.WriteLine("}");
            }
        }

        public void Print_Follows()
        {
            Console.WriteLine("follows:");
            foreach (KeyValuePair<char, HashSet<Symbol>> entry in res_follow)
            {
                Console.Write(entry.Key + " : {");
                foreach (Symbol s in entry.Value)
                {
                    if (s.character == 'ε')
                    {
                        Console.Write("{0}", "Eps. ");
                    }
                    else
                    {
                        Console.Write("{0}", s.character);
                    }
                }
                Console.WriteLine("}");
            }
        }

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

    }
}
