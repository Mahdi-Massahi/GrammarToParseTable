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
        public Sample(int i)
        {
            List<List<Symbol>> rights = new List<List<Symbol>>();
            List<Symbol> symbols = new List<Symbol>();
            rules = new List<Rule>();

            switch (i)
            {
                case 1:
                    #region Sample2
                    // 1st Rule
                    symbols.Add(new Nonterminal('E'));
                    symbols.Add(new Terminal('+'));
                    symbols.Add(new Nonterminal('T'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Nonterminal('T'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('E'), new List<List<Symbol>>(rights)));
                    rights.Clear();

                    // 2nd Rule
                    symbols.Add(new Nonterminal('T'));
                    symbols.Add(new Terminal('*'));
                    symbols.Add(new Nonterminal('F'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Nonterminal('F'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('T'), new List<List<Symbol>>(rights)));
                    rights.Clear();

                    // 3rd Rule
                    symbols.Add(new Terminal('('));
                    symbols.Add(new Nonterminal('E'));
                    symbols.Add(new Terminal(')'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Terminal('i'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('F'), new List<List<Symbol>>(rights)));
                    rights.Clear();
                    #endregion
                    break;

                default:
                    #region Sample1
                    // 1st Rule
                    symbols.Add(new Nonterminal('A'));
                    symbols.Add(new Nonterminal('C'));
                    symbols.Add(new Nonterminal('B'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Nonterminal('C'));
                    symbols.Add(new Terminal('b'));
                    symbols.Add(new Terminal('b'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Nonterminal('B'));
                    symbols.Add(new Terminal('a'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('S'), new List<List<Symbol>>(rights)));
                    rights.Clear();

                    // 2nd Rule
                    symbols.Add(new Terminal('d'));
                    symbols.Add(new Terminal('a'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Nonterminal('B'));
                    symbols.Add(new Nonterminal('C'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('A'), new List<List<Symbol>>(rights)));
                    rights.Clear();

                    // 3rd Rule
                    symbols.Add(new Terminal('g'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Terminal('ε'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('B'), new List<List<Symbol>>(rights)));
                    rights.Clear();

                    // 4th Rule
                    symbols.Add(new Terminal('h'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    symbols.Add(new Terminal('ε'));
                    rights.Add(new List<Symbol>(symbols));
                    symbols.Clear();

                    rules.Add(new Rule(new Nonterminal('C'), new List<List<Symbol>>(rights)));
                    #endregion
                    break;
            }
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
