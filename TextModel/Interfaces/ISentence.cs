using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentence: IEnumerable<ITextItem>
    {
        string Value { get; }
        ITextItem this[int index] { get; }
        int Count();
        void Add(ITextItem item);
        void AddRange(IEnumerable<ITextItem> items);
        bool Remove(ITextItem item);
        Punctuation[] SentenceEndings { get; }
    }
}
