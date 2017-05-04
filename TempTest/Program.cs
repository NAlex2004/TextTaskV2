using System;
using System.Collections.Generic;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;

namespace TempTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IWord w1 = new Word("This");
            WordSymbol s1 = new WordSymbol();
            s1.Value = "i";
            WordSymbol s2 = new WordSymbol('s');
            ITextItem p1 = new Punctuation('-');
            ITextItem p2 = new Punctuation('!');
            IWord w2 = new Word(new [] {new WordSymbol(' '), s1, s2, new WordSymbol(' '), });
            ISentence sentence = new Sentence();
            sentence.Add(w1);
            sentence.Add(w2);
            sentence.Add(p1);
            sentence.Add(new Word(" Sentence"));
            sentence.Add(p2);

            Console.WriteLine(sentence.ToString());
//            IWord w2 = new Word();
        }
    }
}