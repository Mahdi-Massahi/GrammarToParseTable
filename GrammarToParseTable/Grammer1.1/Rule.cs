using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarToParseTable.Classes
{
    public class Rule
    {
        public Nonterminal left { private set; get; }
        public List<List<Symbol>> right { private set; get; }

        public Rule(Nonterminal leftSide, List<List<Symbol>> rightSide)
        {
            left = leftSide;
            right = rightSide;
        }

        /// <summary>
        /// Converts Rule to highlevel Rule as String
        /// </summary>
        /// <returns>Rule as string</returns>
        public override string ToString()
        {
            String buffer = left.character.ToString();
            buffer += " ➝ ";
            foreach (List<Symbol> variable in right)
            {
                foreach (Symbol symbol in variable)
                {
                    buffer += symbol.character.ToString();
                }
                buffer += " | ";
            }
            return buffer.Substring(0, buffer.Length-2);
        }
    }
}
