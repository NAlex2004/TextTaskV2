using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentence: IEnumerable<ITextItem>, ISentenceEndings
    {
        string Value { get; }
        ITextItem this[int index] { get; }
        int Count();
        void Add(ITextItem item);
        void AddRange(IEnumerable<ITextItem> items);
        bool Remove(ITextItem item);
    }
}
