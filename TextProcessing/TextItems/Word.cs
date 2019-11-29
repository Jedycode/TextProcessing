using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;

namespace TextProcessing.TextItems
{
    public class Word: SentenceItem, IWord
    {
        public Word(string symbols) : base(symbols)
        {
        }
        public int Length
        {
            get => Symbols.Length;
        }
    }
}
