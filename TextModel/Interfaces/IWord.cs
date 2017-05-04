using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface IWord: ITextItem, IEnumerable<WordSymbol>
    {
        WordSymbol this[int index] { get; set; }
        int Length { get; }
    }
}
