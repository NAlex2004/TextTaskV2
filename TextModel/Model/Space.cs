using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public struct Space: ITextItem
    {
        public string Value
        {
            get { return " "; }
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
