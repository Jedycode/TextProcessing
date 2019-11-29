using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;

namespace TextProcessing.TextItems
{
    public abstract class SentenceItem: ISentenceItem
    {
        private string _symbols = string.Empty;
        public virtual string Symbols
        {
            get => _symbols;
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Symbols");
                }

                _symbols = value;
            }
        }
        public SentenceItem(string symbols)
        {
            Symbols = symbols;
        }
        public override string ToString()
        {
            return $"{Symbols}";
        }
    }
}
