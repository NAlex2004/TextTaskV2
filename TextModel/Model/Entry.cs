using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Entry: IEntry<IWord, int>
    {
        private IEnumerable<int> _positions;

        public IWord Key
        {
            get;
            private set;
        }

        public int EntryCount
        {
            get;
            private set;
        }

        public IEnumerable<int> EntryPositions
        {
            get { return _positions; }
        }

        public Entry(IWord key, IEnumerable<int> entries)
        {
            Key = key;
            _positions = entries;
            if (_positions != null)
                EntryCount = _positions.Count();
        }


        public string EntryValue
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(EntryCount).Append(":  ");
                foreach (var position in _positions)
                    sb.Append(position).Append(' ');

                return sb.ToString();
            }
        }
    }
}
