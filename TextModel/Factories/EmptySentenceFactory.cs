using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Factories
{
    public class EmptySentenceFactory: ISentenceFactory
    {
        public IList<ITextItem> CreateSentenceItems()
        {
            return new List<ITextItem>();
        }

        public Punctuation[] CreateSentenceEndings()
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
