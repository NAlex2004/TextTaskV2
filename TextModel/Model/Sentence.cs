﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Sentence: ISentence
    {
        protected ICollection<ITextItem> items;

        public Sentence()
        {
            items = new List<ITextItem>();
        }

        public Sentence(ISentenceFactory factory)
        {
            items = factory.CreateSentenceItems();
        }

        public IEnumerator<ITextItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Value
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (ITextItem item in items)
                {
                    sb.Append(item.Value);
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public ITextItem this[int index]
        {
            get { return items.ElementAt(index); }
        }

        public int Count()
        {
            return items.Count();
        }

        public void Add(ITextItem item)
        {
            items.Add(item);
        }

        public void AddRange(IEnumerable<ITextItem> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
        }

        public bool Remove(ITextItem item)
        {
            return items.Remove(item);
        }

        public Punctuation[] GetSentenceEndings()
        {
            return new Punctuation[]
            {
                new Punctuation('.'),
                new Punctuation('!'),
                new Punctuation('?')
            };
        }
    }
}