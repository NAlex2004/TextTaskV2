using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ITextPage: IEnumerable<IEnumerable<ISentence>>
    {
        int LinesPerPage { get; }
    }
}
