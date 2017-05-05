using System;
using System.Collections.Generic;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;
using NAlex.TextModel.Parsers;
using NAlex.TextModel.Factories;
using System.IO;
using System.Linq;
using System.Text;

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
            IWord w2 = new Word(new [] { s1, s2 });
            ISentence sentence = new Sentence();
            sentence.Add(w1);
            sentence.Add(w2);
            sentence.Add(new Space());
            sentence.Add(p1);
            sentence.Add(new Space());
            sentence.Add(new Word("Sentence"));
            sentence.Add(p2);
            sentence.AddRange(new ITextItem[] { new Punctuation('!'), new Punctuation('?')});
            Console.WriteLine(sentence);

            string text = File.ReadAllText("test.txt", Encoding.UTF8);
            IParser parser = new LineParser(text, new EmptySentenceFactory());
            ICollection<ISentence> model = parser.GetTextModel();
            //model.SelectMany(s => s.Select(i => i.Value)).ToList().ForEach(s => Console.WriteLine(s));
            foreach (var sent in model)
                Console.Write(sent);
            Console.ReadKey();
        }
    }
}