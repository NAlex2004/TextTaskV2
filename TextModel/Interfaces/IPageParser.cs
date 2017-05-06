using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;
using System.IO;

namespace NAlex.TextModel.Interfaces
{
    public interface IPageParser
    {
        IEnumerable<ITextPage> GetTextPages(Stream stream, int linesPerPage, ISentenceFactory sentenceFactory);
    }
}
