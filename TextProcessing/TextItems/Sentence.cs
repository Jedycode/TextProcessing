using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
using System.Linq;

namespace TextProcessing.TextItems
{
    public class Sentence : ISentence
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

        public Sentence(IEnumerable<ISentenceItem> sentenceItem) : this()
        {
            SentenceItems.AddRange(sentenceItem);
        }
        public int Count
        {
            get => SentenceItems.Count;
        }

        public ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceItem
        {
            return selector == null ?
                new List<T>(SentenceItems.OfType<T>().ToList()) :
                new List<T>(SentenceItems.OfType<T>().Where(selector).ToList());
        }

        public bool IsInterrogative()
        {
            if (SentenceItems.Last() is ISeparator lastElement)
            {
                return lastElement.IsQuestionMark();
            }

            return false;
        }

        public ICollection<ISentenceItem> RemoveAll<T>(Predicate<T> predicate) where T : ISentenceItem
        {
            var resultCollection = new List<ISentenceItem>(SentenceItems.ToList());
            var appropriateElements = GetAppropriateElements(predicate);

            if (appropriateElements.Any())
            {
                RemoveWords(appropriateElements, resultCollection);
            }

            return resultCollection;
        }

        public ICollection<ISentenceItem> InsertInsteadOf<T>(Predicate<T> predicate,
            IList<ISentenceItem> elements)
            where T : ISentenceItem
        {
            var resultCollection = new List<ISentenceItem>(SentenceItems.ToList());
            var appropriateElements = GetAppropriateElements(predicate);

            if (appropriateElements.Any())
            {
                foreach (var item in appropriateElements)
                {
                    int index = RemoveWord(item, resultCollection, true);
                    resultCollection = InsertRange(index, elements, resultCollection);
                }
            }

            return resultCollection;
        }


        private int RemoveWord(ISentenceItem word, IList<ISentenceItem> collection, bool toInsert = false)
        {
            var index = collection.IndexOf(word);

            bool lastButNotSole = (index == collection.Count - 2) && (index > 0);

            bool lastBeforeRemoving = (collection.OfType<Word>().Last().Equals(word));

            collection.Remove(word);

            if (toInsert)
            {
                if (!lastBeforeRemoving)
                {
                    RemoveSeparatorBeforeWord(lastButNotSole, index, collection);
                }
            }
            else
            {
                RemoveSeparatorBeforeWord(lastButNotSole, index, collection);
            }

            return index;
        }

        private void RemoveSeparatorBeforeWord(bool lastButNotSole, int index, IList<ISentenceItem> collection)
        {
            //If it is last word and there are more then 1 words in sentence,
            //then remove separator before word
            if (lastButNotSole)
            {
                index--;
            }
            collection.RemoveAt(index);
        }

        private void RemoveWords<T>(ICollection<T> words, IList<ISentenceItem> collection) where T : ISentenceItem
        {
            foreach (var item in words)
            {
                RemoveWord(item, collection);
            }
        }

        private ICollection<T> GetAppropriateElements<T>(Predicate<T> predicate)
        {
            return SentenceItems.OfType<T>().ToList().FindAll(predicate);
        }

        private List<ISentenceItem> InsertRange(int index,
            IList<ISentenceItem> elementsToInsert,
            List<ISentenceItem> collection)
        {
            var currentIndex = collection.Count;

            collection.InsertRange(index, elementsToInsert);

            if (collection.Last().Equals(collection[index + elementsToInsert.Count]))
            {
                collection.RemoveAt(index + elementsToInsert.Count - 1);
            }

            return collection;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in SentenceItems)
            {
                stringBuilder.Append(item.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}

