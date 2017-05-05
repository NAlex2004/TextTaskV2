﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ITextPage: IEnumerable<ITextLine>
    {
        int PageNumber { get; }
        int LinesPerPage { get; }
    }
}
