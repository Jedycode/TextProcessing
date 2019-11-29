using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;

namespace TextProcessing.TextItems
{
    public class Sentence
    {
        private List<ISentenceItem> _sentenceItems;

        public List<ISentenceItem> SentenceItems
        {
            get => _sentenceItems;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SentenceElement");
                }

                _sentenceItems = value;
            }
        }
        private Sentence()
        {
            SentenceItems = new List<ISentenceItem>();
        }

        public List<ISentenceItem> AddSentenceItem(ISentenceItem sentenceItem)
        {
            SentenceItems.Add(sentenceItem);
            return SentenceItems;

        }
    }
}
