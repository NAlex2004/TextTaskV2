using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NAlex.TextModel.Interfaces
{
    public interface ICorcordance<TKey, TEntry>: IEnumerable<KeyValuePair<TKey, IEnumerable<TEntry>>>
    {
        IEnumerable<TEntry> this[TKey key] { get; }
        void AddEntry(TEntry entry);
        bool RemoveEntry(TEntry entry);
        Stream ToStream();
    }
}
