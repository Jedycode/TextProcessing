using System;
using System.Collections.Generic;
using System.Text;
using TextProcessing.Interfaces;
namespace TextProcessing.Interfaces
{
    public interface ISentence
    {
        int Count { get; }

        ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceItem;


        ICollection<ISentenceItem> RemoveAll<T>(Predicate<T> predicate) where T : ISentenceItem;

        ICollection<ISentenceItem> InsertInsteadOf<T>(Predicate<T> predicate,
            IList<ISentenceItem> elements)
            where T : ISentenceItem;
    }
}
}
