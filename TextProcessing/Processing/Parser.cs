using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
using TextProcessing.TextItems;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace TextProcessing.Processing
{
    public class Parser
    {
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

            ICollection<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line, "[\f\n\r\t\v]", " ");

                sentenceItems = ParseLine(line, sentenceItems, text);
            }
        }

        public List<ISentenceItem> ParseLine(string line)
        {
            return ParseLine(line, null, null);
        }

        private List<ISentenceItem> ParseLine(string line, ICollection<ISentenceItem> sentenceItems, IText text = null)
        {
            line = String.Concat(line, " ");

            if (sentenceItems == null)
            {
                sentenceItems = new List<ISentenceItem>();
            }

            string[] sentenceSeparators = new string[] { "...", ".", "!?", "?!", "!", "?" };

            string pattern = @"\b(\w+)((\p{P}{0,3})\s?)";

            foreach (Match match in Regex.Matches(line, pattern))
            {
                sentenceItems.Add(new Word(match.Groups[1].ToString()));

                sentenceItems.Add(new Separator(match.Groups[2].ToString()));

                var res = sentenceSeparators.Any(x => x.Equals(match.Groups[2].ToString().TrimEnd(' ')));

                if (res)
                {
                    CreateSentence(text, sentenceItems);
                    sentenceItems = new List<ISentenceItem>();
                }
            }

            return sentenceItems.ToList();
        }

        private void CreateSentence(IText text, ICollection<ISentenceItem> sentenceItems)
        {
            if (text != null)
            {
                text.Add(new Sentence(sentenceItems));
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

