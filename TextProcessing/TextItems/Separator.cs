using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
using System.Runtime.Serialization;

namespace TextProcessing.TextItems
{
    public class Separator: SentenceItem, ISeparator
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
