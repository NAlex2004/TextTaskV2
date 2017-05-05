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
        ISentence sentence;
        IWord word;
        string text;
        ISentenceFactory factory;

        public LineParser(string line, ISentenceFactory sentenceFactory)
        {
            text = line;
            factory = sentenceFactory;
        }

        ITextItem CreateNonWordItem(char ch)
        {
            if (char.IsPunctuation(ch))
                return new Punctuation(ch);
            else
                return new Space();
        }

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

        public ICollection<ISentence> GetTextModel()
        {
            ICollection<ISentence> list = new List<ISentence>();
            ICollection<WordSymbol> symbols = new List<WordSymbol>();
            sentence = new Sentence(factory);

            if (string.IsNullOrEmpty(text))
                return list;

            char tab = '\t';

            int i = 0;
            while (i < text.Length)
            {
                // NewLines ???

                char ch = text[i].Equals(tab) ? ' ' : text[i];

                // skip spaces & tabs
                Skip(ref i, c => c.Equals(' ') || c.Equals(tab));
                //while (i < (text.Length - 1) && (text[i+1].Equals(' ') || text[i+1].Equals(tab)))
                //    i++;                

                if (ch.Equals(' '))
                {
                    if (symbols.Count > 0)
                    {
                        sentence.Add(new Word(symbols));
                        symbols.Clear();
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
                            symbols.Clear();
                        }

                        sentence.Add(last);
                        if (marks.Count() > 0)
                        {
                            sentence.AddRange(marks);
                            last = (Punctuation)marks.ElementAt(marks.Count() - 1);
                        }

                        // sentence end
                        if (sentence.SentenceEndings.Contains(last))
                        {
                            list.Add(sentence);
                            sentence = new Sentence(factory);
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
