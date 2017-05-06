using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Corcordance: ICorcordance<WordSymbol, IEntry<IWord, int>>
    {
        private IDictionary<WordSymbol, ICollection<IEntry<IWord, int>>> corcordanceEntries = 
            new Dictionary<WordSymbol, ICollection<IEntry<IWord, int>>>();

        public IEnumerable<IEntry<IWord, int>> this[WordSymbol key]
        {
            get { return corcordanceEntries[key]; }
        }

        public void AddEntry(IEntry<IWord, int> entry)
        {
            if (entry == null || entry.Key == null)
                return;

            WordSymbol key = new WordSymbol(entry.Key.Value[0]);

            if (corcordanceEntries.Keys.Contains(key))
            {
                // entry does not exists
                if (corcordanceEntries[key].Where(e => e.Key.Equals(entry.Key)).FirstOrDefault() != null)
                    corcordanceEntries[key].Add(entry);
            }
            else
            {
                corcordanceEntries.Add(new KeyValuePair<WordSymbol,ICollection<IEntry<IWord,int>>>(key, new List<IEntry<IWord, int>>() {entry}));
            }
        }

        public bool RemoveEntry(IEntry<IWord, int> entry)
        {
            if (entry == null || entry.Key == null)
                return false;

            WordSymbol key = new WordSymbol(entry.Key.Value[0]);

            if (corcordanceEntries.Keys.Contains(key))
                return corcordanceEntries[key].Remove(entry);
            else
                return false;
        }

        public System.IO.Stream GetCorcordance()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<WordSymbol, IEnumerable<IEntry<IWord, int>>>> GetEnumerator()
        {
            foreach (var key in corcordanceEntries.Keys)
                yield return new KeyValuePair<WordSymbol, IEnumerable<IEntry<IWord, int>>>(key, corcordanceEntries[key]);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
