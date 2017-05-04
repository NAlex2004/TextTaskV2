using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public static class SentenceEndings: ISentenceEndings
    {
        public static Punctuation[] GetSentenceEndings()
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
