using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Classes
{
    public class Nonterminal : Symbol
    {
        /// <summary>
        /// A Nonterminal variable
        /// </summary>
        /// <param name="ch">Must be an uppercase character</param>
        public Nonterminal(char ch) : base(ch)
        {
            if (!Char.IsUpper(ch))
            {
                throw new Exception("Nonterminal must be an uppercase character.");
            }
        }
    }
}
