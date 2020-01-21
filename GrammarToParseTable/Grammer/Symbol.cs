using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Classes
{
    public class Symbol
    {
        public char character {private set; get;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch">A character as a variable</param>
        public Symbol(char ch)
        {
            character = ch;
        }

        public override int GetHashCode()
        {
            return character;
        }

        public override bool Equals(object obj)
        {
            if (obj is Symbol)
            {
                return character == ((Symbol)obj).character;
            }
            return false;
        }
    }
}
