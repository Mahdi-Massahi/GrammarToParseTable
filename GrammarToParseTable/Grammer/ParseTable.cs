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
            follows = Follow.FindAllFollows(rules, firsts);
        }

        public void Print_Firsts()
        {
            Console.WriteLine("firsts:");
            foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in firsts)
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
            foreach (KeyValuePair<Rule, HashSet<Symbol>> entry in follows)
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
            for (int i = 0; i < firsts.Count; i++)
            {
                Rule r = firsts.ElementAt(i).Key;
                data.Add(new FiFoItem()
                {
                    Number = i+1,
                    Production = r.ToString(),
                    First = SymbolHashSetToString(firsts[r]),
                    Follow = SymbolHashSetToString(follows[r])
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
