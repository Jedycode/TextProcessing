using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TextProcessing.Interfaces;

namespace TextProcessing.Interfaces
{
    public interface IParser
    {
        void Parse(IText text, StreamReader sr);
        ICollection<ISentenceItem> ParseLine(string line);

    }
}
