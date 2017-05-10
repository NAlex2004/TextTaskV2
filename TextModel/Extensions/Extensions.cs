using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;

namespace NAlex.TextModel.Extensions
{
    public static class Extensions
    {
        static char[] vowels = { 'e', 'y', 'u', 'i', 'o', 'a', 
                                'у', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю', 'ё'};
        public static bool IsVowel(this char ch)
        {
            char c = char.ToLower(ch);
            return vowels.Contains(char.ToLower(ch));
        }
    }
}
