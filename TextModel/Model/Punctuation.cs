using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public struct Punctuation : ITextItem
    {
        private char _value;

        public Punctuation(char value)
        {
            if (!char.IsPunctuation(value))
                throw new ArgumentException(string.Format("'{0}' is not a punctuation mark.", value));
            _value = value;
        }

        public string Value
        {
            get { return _value.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value) && char.IsPunctuation(value[0]))
                {
                    _value = value[0];
                }
            }
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
