using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NAlex.TextModel.Factories;
using NAlex.TextModel.Parsers;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class CorcordanceFileBuilder : ICorcordanceBuilder<WordSymbol, IEntry<IWord, int>>
    {
        private readonly string _inputFile;
        private readonly int _linesPerPage;

        public CorcordanceFileBuilder(string inputFile, int linesPerPage)
        {
            _inputFile = inputFile;
            _linesPerPage = linesPerPage;
        }

        public ICorcordance<WordSymbol, IEntry<IWord, int>> BuildCorcordance()
        {
            ICorcordance<WordSymbol, IEntry<IWord, int>> corcordance = new Corcordance();
            IEnumerable<ITextPage> pages = null;

            try
            {
                FileStream stream = new FileStream(_inputFile, FileMode.Open);

                IPageParser parser = new PageParser();
                pages = parser.GetTextPages(stream, _linesPerPage, new EmptySentenceFactory());

                stream.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            var pageWords = pages.Select(p => new
                {
                    p.PageNumber,
                    Words = p.SelectMany(a => a.SelectMany(s => s.OfType<IWord>()))
                })
                .SelectMany(p => p.Words, (p, w) => new {p.PageNumber, PageWord = w})
                .GroupBy(p => p.PageWord, x => x.PageNumber);

            foreach (var pageWord in pageWords)
            {
                var wordPages = pageWord.ToArray();
                IEntry<IWord, int> entry = new Entry(pageWord.Key, wordPages);
                corcordance.AddEntry(entry);
            }

            return corcordance;
        }
    }
}