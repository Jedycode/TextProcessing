using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessing.Interfaces
{
    public interface IText
    {
        void Add(ISentence sentence);

        ISentence GetSentenceById(int index);

    }
}
