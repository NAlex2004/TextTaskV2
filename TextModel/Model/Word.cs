using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Word: IWord
    {
        protected IEnumerable<WordSymbol> word;

        public Word(IEnumerable<WordSymbol> value)
        {
            if (value != null)
                word = value;
            else
                word = new List<WordSymbol>();
        }

        /// <summary>
        /// Create Word from string. All punctuation marks will be ignored.
        /// List will be used as a collection.
        /// </summary>
        public Word(string value)
        {
            List<WordSymbol> list = new List<WordSymbol>();
            if (!string.IsNullOrEmpty(value))
            {
                var wordSymbols = value.Where(c => !char.IsPunctuation(c));
                foreach (var c in wordSymbols)
                {
                    list.Add(new WordSymbol(c));
                }
            }
            word = list;
        }

        public WordSymbol this[int index]
        {
            get
            {
                return word.ElementAt(index);
            }
        }

        public int Length
        {
            get { return word.Count(); }
        }

        public string Value
        {
            get 
            {
                return new string(word.SelectMany(c => c.Value).ToArray());
            }
        }

        public IEnumerator<WordSymbol> GetEnumerator()
        {
            return word.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
