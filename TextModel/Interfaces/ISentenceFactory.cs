using System.Collections.Generic;

namespace NAlex.TextModel.Interfaces
{
    public interface ISentenceFactory
    {
        ICollection<ITextItem> CreateSentenceItems();
    }
}