using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.TextModel.Interfaces
{
    public interface IParser
    {
        ICollection<ISentence> GetTextModel();
    }
}
