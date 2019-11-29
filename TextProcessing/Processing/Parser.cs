using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
using TextProcessing.TextItems;
using System.IO;
using System.Text.RegularExpressions;

namespace TextProcessing.Processing
{
    class Parser: IParser
    {
        public IList<ISentenceItem> ParseLine(string line)
        {
            return ParseLine(line, null, null);
        }
        public void Parse(IText text, StreamReader sr)
        {
            if (sr == null)
            {
                throw new ArgumentNullException("StreamReader");
            }
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            string line;

            ICollection<ISentenceItem> elements = new List<ISentenceItem>();

            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line, "[\f\n\r\t\v]", " ");

                elements = ParseLine(line, elements, text);
            }
        }
        private void CreateSentence(IText text, ICollection<ISentenceItem> elements)
        {
            if (text != null)
            {
                text.Add(new Sentence(elements));
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
