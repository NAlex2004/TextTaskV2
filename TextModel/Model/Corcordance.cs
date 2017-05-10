using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;
using System.IO;

namespace NAlex.TextModel.Model
{
    public class Corcordance: ICorcordance<WordSymbol, IEntry<IWord, int>>
    {
        private readonly IDictionary<WordSymbol, ICollection<IEntry<IWord, int>>> corcordanceEntries =
            new Dictionary<WordSymbol, ICollection<IEntry<IWord, int>>>();

        public IEnumerable<IEntry<IWord, int>> this[WordSymbol key]
        {
            get { return corcordanceEntries[key]; }
        }

        public void AddEntry(IEntry<IWord, int> entry)
        {
            if (entry == null || entry.Key == null)
                return;

            WordSymbol key = new WordSymbol(entry.Key.Value.ToUpper()[0]);

            if (corcordanceEntries.Keys.Contains(key))
            {
                var existingEntry = corcordanceEntries[key]
                    .FirstOrDefault(e => e.Key.Value.Equals(entry.Key.Value, StringComparison.OrdinalIgnoreCase));
                // entry does not exists
                if (existingEntry == null)
                    corcordanceEntries[key].Add(entry);
                else
                {
                    // replace existing entry
                    if (corcordanceEntries[key].Remove(existingEntry))
                        corcordanceEntries[key].Add(entry);
                }
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

            WordSymbol key = new WordSymbol(entry.Key.Value.ToUpper()[0]);

            if (corcordanceEntries.Keys.Contains(key))
            {
                bool res = corcordanceEntries[key].Remove(entry);
                // no more entries left
                if (corcordanceEntries[key].FirstOrDefault() == null)
                    corcordanceEntries.Remove(key);
                return res;
            }
            else
                return false;
        }

        public Stream ToStream()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            foreach (var key in corcordanceEntries.Keys.OrderBy(k => k))
            {
                writer.WriteLine(" {0}:", key);
                writer.WriteLine();

                foreach (var entry in corcordanceEntries[key].OrderBy(e => e.Key))
                {
                    string s = entry.Key.Value.ToLower();

                    writer.WriteLine("{0}.... {1}", entry.Key.Value.ToLower().PadRight(60, '.'), entry.EntryValue);
                }
                writer.WriteLine();
                writer.WriteLine();
            }
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
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
