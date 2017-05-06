using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ITextPage: IEnumerable<ICollection<ISentence>>
    {
        int PageNumber { get; }
        int LinesPerPage { get; }
        int Count { get; }

        ICollection<ISentence> this[int index] { get; set; }
        void Add(ICollection<ISentence> item);
        bool Remove(ICollection<ISentence> item);
        void RemoveAt(int index);
        void Clear();
    }
}
