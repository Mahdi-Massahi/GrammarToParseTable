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

    }
}
