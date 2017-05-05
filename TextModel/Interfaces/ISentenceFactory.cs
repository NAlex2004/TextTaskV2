using System.Collections.Generic;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentenceFactory
    {
        IList<ITextItem> CreateSentenceItems();
        Punctuation[] CreateSentenceEndings();
    }
}