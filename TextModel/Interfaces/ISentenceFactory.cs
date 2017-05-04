using System.Collections.Generic;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentenceFactory
    {
        ICollection<ITextItem> CreateSentenceItems();
        Punctuation[] CreateSentenceEndings();
    }
}