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
        ITextItem this[int index] { get; set; }
        int Count();
        void Add(ITextItem item);
        void AddRange(IEnumerable<ITextItem> items);
        bool Remove(ITextItem item);
        bool Replace(ITextItem oldItem, IEnumerable<ITextItem> newItems);
        bool Replace(Func<ITextItem, bool> searchCondition, IEnumerable<ITextItem> newItems);
        Punctuation[] SentenceEndings { get; }
    }
}
