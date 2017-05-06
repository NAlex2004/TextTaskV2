using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.TextModel.Interfaces
{
    public interface IEntry<out TKey, out TPositions>
    {
        TKey Key { get; }
        int EntryCount { get; }
        string EntryValue { get; }
        IEnumerable<TPositions> EntryPositions { get; }        
        //void AddPosition(TPositions position);
        //bool RemovePosition(TPositions position);
    }
}
