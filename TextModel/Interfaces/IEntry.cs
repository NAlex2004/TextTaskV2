using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.TextModel.Interfaces
{
    public interface IEntry<TKey, TPositions>
    {
        TKey Key { get; }
        int EntryCount { get; }
        IEnumerable<TPositions> EntryPositions { get; }
    }
}
