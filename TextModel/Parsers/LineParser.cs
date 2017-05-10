using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Parsers
{
    public class LineParser: IParser
    {        
        string text;

        ITextItem CreateNonWordItem(char ch)
        {
            if (char.IsPunctuation(ch))
                return new Punctuation(ch);
            else
                return new Space();
        }

        /// <summary>
        /// Skip non-word items (spaces, punctuation marks) in text by condition
        /// </summary>        
        /// <returns>IEnumerable of skipped items</returns>
        IEnumerable<ITextItem> Skip(ref int index, Func<char, bool> condition)
        {
            ICollection<ITextItem> items = new List<ITextItem>();

            while (index < (text.Length - 1) && condition(text[index + 1]))
            {
                items.Add(CreateNonWordItem(text[index + 1]));
                index++;
            }

            return items;
        }

        /// <summary>
        /// Create text model from text line. New line chars become part of a word.
        /// Use PageParser to parse multiline text.
        /// </summary>        
        /// <returns>collection of text sentencies</returns>
        public ICollection<ISentence> GetTextModel(string line, ISentenceFactory sentenceFactory)
        {
            ICollection<ISentence> list = new List<ISentence>();
            ICollection<WordSymbol> symbols = new List<WordSymbol>();
            ISentence sentence = new Sentence(sentenceFactory);

            text = line;

            if (string.IsNullOrEmpty(text))
                return list;

            char tab = '\t';

            int i = 0;
            while (i < text.Length)
            {
                char ch = text[i].Equals(tab) ? ' ' : text[i];                     

                if (ch.Equals(' '))
                {
                    // skip additional spaces & tabs
                    Skip(ref i, c => c.Equals(' ') || c.Equals(tab));

                    if (symbols.Count > 0)
                    {
                        sentence.Add(new Word(symbols));
                        symbols = new List<WordSymbol>();
                    }
                    sentence.Add(new Space());
                }
                else
                {
                    if (char.IsPunctuation(ch))
                    {
                        Punctuation last = new Punctuation(ch);
                        // get all consistent punctuation marks
                        var marks = Skip(ref i, c => char.IsPunctuation(c));
                        if (symbols.Count > 0)
                        {
                            sentence.Add(new Word(symbols));
                            symbols = new List<WordSymbol>();
                        }

                        sentence.Add(last);
                        if (marks.Count() > 0)
                        {
                            sentence.AddRange(marks);
                            last = (Punctuation)marks.ElementAt(marks.Count() - 1);
                        }
                        
                        // sentence end. space must follow the end of the sentence
                        if (sentence.SentenceEndings.Contains(last) && i < (text.Length - 1) && text[i + 1].Equals(' '))
                        {
                            list.Add(sentence);
                            sentence = new Sentence(sentenceFactory);
                        }
                    }
                    else
                    {
                        symbols.Add(new WordSymbol(ch));
                    }
                }
                

                i++;
            }

            if (symbols.Count > 0)
                sentence.Add(new Word(symbols));
            if (sentence.Count() > 0)
                list.Add(sentence);

            return list;
        }
    }
}
