using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NAlex.TextModel.Model;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Parsers
{
    public class PageParser: IPageParser
    {
        public IEnumerable<ITextPage> GetTextPages(Stream stream, int linesPerPage, ISentenceFactory sentenceFactory)
        {
            if (linesPerPage < 1)
                throw new ArgumentException("linesPerPage must be greater than zero.");

            IList<ITextPage> pages = new List<ITextPage>();

            if (stream != null && sentenceFactory != null)
            {
                IParser lineParser = new LineParser();

                using (StreamReader reader = new StreamReader(stream))
                {
                    string str;
                    int pageNumber = 1;
                    int lineNumber = 0;
                    ITextPage page = new Page(linesPerPage, pageNumber);
                    ICollection<ISentence> line;

                    while ((str = reader.ReadLine()) != null)
                    {
                        line = lineParser.GetTextModel(str, sentenceFactory);
                        page.Add(line);
                        lineNumber++;
                        if (lineNumber >= linesPerPage)
                        {
                            lineNumber = 0;
                            pageNumber++;
                            pages.Add(page);
                            page = new Page(linesPerPage, pageNumber);
                        }
                    }

                    if (page.Count > 0)
                        pages.Add(page);
                }

            }

            return pages;
        }
    }
}
