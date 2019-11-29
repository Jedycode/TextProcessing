using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessing.TextItems
{
    public class Separator: SentenceItem
    {
        public Separator(string symbols) : base(symbols)
        {
        }
        public bool IsQuestionMark()
        {

            return Symbols.Contains("?");

        }
    }
}
