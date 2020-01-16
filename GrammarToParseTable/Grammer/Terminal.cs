using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Classes
{
    public class Terminal : Symbol
    {
        /// <summary>
        /// A variable as Terminal
        /// </summary>
        /// <param name="ch">Must ber in lowercase</param>
        public Terminal(char ch) : base(ch)
        {
            if (Char.IsUpper(ch))
            {
                throw new Exception("Terminal must be a lowercase character.");
            }
        }
    }
}
