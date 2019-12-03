using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TextProcessing.Interfaces;
using TextProcessing.CharCheck;
using System.Runtime.Serialization;

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
        public bool StartWithConsonant()
        {
            return Symbols.FirstOrDefault().IsConsonant();
        }
    }
}
