using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentence: IEnumerable<ITextItem>
    {
        string Value { get; }
        ITextItem this[int index] { get; set; }
        int Count();
        void Add(ITextItem item);
        bool Remove(ITextItem item);
    }
}
