using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Sentence: ISentence
    {
        protected IList<ITextItem> items; 
        protected Punctuation[] sentenceEndings;

        public Sentence()
        {
            items = new List<ITextItem>();
            sentenceEndings = new Punctuation[]
            {
                new Punctuation('.'),
                new Punctuation('!'),
                new Punctuation('?')
            };
        }

        public Sentence(ISentenceFactory factory)
        {
            items = factory.CreateSentenceItems();
            sentenceEndings = factory.CreateSentenceEndings();
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
            get 
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
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

        public Punctuation[] SentenceEndings
        {
            get { return sentenceEndings; }
        }

        public bool Replace(ITextItem oldItem, IEnumerable<ITextItem> newItems)
        {
            bool result = false;

            if (newItems != null)
            {
                int index = items.IndexOf(oldItem);
                if (index >= 0)
                {
                    items.RemoveAt(index);
                    foreach (var item in newItems)
                    {
                        items.Insert(index, item);
                        index++;
                    }
                    result = true;
                }
            }

            return result;
        }

        public bool Replace(Func<ITextItem, bool> searchCondition, IEnumerable<ITextItem> newItems)
        {
            bool result = true;

            var oldItems = items.Where(searchCondition).ToArray();

            if (oldItems.Length > 0)
            {
                foreach (var item in oldItems)
                {
                    result &= Replace(item, newItems);
                }

                return result;
            }
                
            return false;
        }
    }
}