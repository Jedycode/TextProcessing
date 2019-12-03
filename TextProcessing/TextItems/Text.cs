using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace TextProcessing.TextItems
{
    public class Text: IText
    {
        private ICollection<ISentence> _sentences = new List<ISentence>();
        public ICollection<ISentence> Sentences
        {
            get => _sentences;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Sentence");
                }

                _sentences = value;
            }
        }
        public void Add(ISentence sentence)
        {
            if (sentence == null)
            {
                throw new ArgumentNullException("Sentence");
            }

            _sentences.Add(sentence);
        }
        public ISentence GetSentenceById(int index)
        {
            if (index < 0 || index >= _sentences.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return _sentences.ElementAt(index);
        }
        public ICollection<ISentence> GetSentences(Func<ISentence, bool> selector = null)
        {
            return selector == null ?
                new List<ISentence>(_sentences) :
                new List<ISentence>(_sentences.Where(selector).ToList());
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in _sentences)
            {
                stringBuilder.Append(item.ToString());
            }

            return stringBuilder.ToString();
        }




    }
}
