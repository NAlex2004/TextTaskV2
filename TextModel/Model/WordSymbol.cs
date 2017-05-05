using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;
using System.Globalization;

namespace NAlex.TextModel.Model
{
    /// <summary>
    /// Non-punctuation symbol
    /// </summary>
    public struct WordSymbol : IComparable<WordSymbol>
    {
        private char _value;

        public WordSymbol(char value)
        {
            if (char.IsPunctuation(value) || value == ' ')
                throw new ArgumentException(string.Format("'{0}' is a punctuation mark or space.", value));
            _value = value;
        }

        public string Value
        {
            get { return _value.ToString(); }
            set
            {
                string str = value.Trim();
                if (!string.IsNullOrEmpty(str) && !char.IsPunctuation(str[0]))
                    _value = str[0];
            }
        }

        public WordSymbol ToUpper()
        {
            return new WordSymbol(char.ToUpper(_value));
        }

        public WordSymbol ToUpper(CultureInfo culture)
        {
            return new WordSymbol(char.ToUpper(_value, culture));
        }

        public WordSymbol ToLower()
        {
            return new WordSymbol(char.ToLower(_value));
        }

        public WordSymbol ToLower(CultureInfo culture)
        {
            return new WordSymbol(char.ToLower(_value, culture));
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(WordSymbol other)
        {
            return _value.CompareTo(other.Value[0]);
        }

    }
}
