using GrammarToParseTable.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Grammer
{
    /// <summary>
    /// Contains a sample garmmar press Ctrl
    /// </summary>
    class Sample
    {

        //  Production Rules:
        //  S -> ACB|Cbb|Ba
        //  A -> da|BC
        //  B-> g|Є
        //  C-> h| Є
        //  -------------
        //  FIRST set
        //  FIRST(S) = FIRST(A) U FIRST(B) U FIRST(C) = { d, g, h, Є, b, a }
        //  FIRST(A) = { d } U FIRST(B) = { d, g, Є }
        //  FIRST(B) = { g, Є }
        //  FIRST(C) = { h, Є }
        //  -------------
        //  FOLLOW Set
        //  FOLLOW(S) = { $ }
        //  FOLLOW(A)  = { h, g, $ }
        //  FOLLOW(B) = { a, $, h, g }
        //  FOLLOW(C) = { b, g, $, h }

        public List<Rule> rules { private set; get; }

        /// <summary>
        /// Creates a sample List<Rule>
        /// </summary>
        public Sample()
        {
            List<List<Symbol>> rights = new List<List<Symbol>>();
            List<Symbol> symbols = new List<Symbol>();
            rules = new List<Rule>();

            // 1st Rule
            symbols.Add(new Symbol('A'));
            symbols.Add(new Symbol('C'));
            symbols.Add(new Symbol('B'));
            rights.Add(symbols);
            symbols.Clear();

            symbols.Add(new Symbol('C'));
            symbols.Add(new Symbol('b'));
            symbols.Add(new Symbol('b'));
            rights.Add(symbols);
            symbols.Clear();

            symbols.Add(new Symbol('B'));
            symbols.Add(new Symbol('a'));
            rights.Add(symbols);
            symbols.Clear();

            rules.Add(new Rule(new Nonterminal('S'), rights));
            rights.Clear();

            // 2nd Rule
            symbols.Add(new Symbol('d'));
            symbols.Add(new Symbol('a'));
            rights.Add(symbols);
            symbols.Clear();

            symbols.Add(new Symbol('B'));
            symbols.Add(new Symbol('C'));
            rights.Add(symbols);
            symbols.Clear();

            rules.Add(new Rule(new Nonterminal('A'), rights));
            rights.Clear();

            // 3rd Rule
            symbols.Add(new Symbol('g'));
            symbols.Add(new Symbol('ε'));
            rights.Add(symbols);
            symbols.Clear();

            rules.Add(new Rule(new Nonterminal('B'), rights));
            rights.Clear();

            // 4th Rule
            symbols.Add(new Symbol('h'));
            symbols.Add(new Symbol('ε'));
            rights.Add(symbols);
            symbols.Clear();

            rules.Add(new Rule(new Nonterminal('C'), rights));
        }

        /// <summary>
        /// Returns the Sample
        /// </summary>
        /// <returns>A Sample Rules</returns>
        public List<Rule> get()
        {
            return rules;
        }
    }
}
