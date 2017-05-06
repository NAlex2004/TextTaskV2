using System;
using System.Collections.Generic;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;
using NAlex.TextModel.Parsers;
using NAlex.TextModel.Factories;
using System.IO;
using System.Linq;
using System.Text;
using NAlex.TextModel.Extensions;

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
            sentence.Add(new Space());
            sentence.Add(w2);
            sentence.Add(new Space());
            sentence.Add(p1);
            sentence.Add(new Space());
            sentence.Add(new Word("Sentence"));
            sentence.Add(p2);
            sentence.AddRange(new ITextItem[] { new Punctuation('!'), new Punctuation('?')});
            Console.WriteLine(sentence);

            string text = File.ReadAllText("test.txt", Encoding.UTF8);
            IParser parser = new LineParser();
            ICollection<ISentence> model = parser.GetTextModel(text, new EmptySentenceFactory());
            //model.SelectMany(s => s.Select(i => i.Value)).ToList().ForEach(s => Console.WriteLine(s));
            //foreach (var sent in model)
            //    Console.Write(sent);
            
            // по возрастанию кол-ва слов
            model.OrderBy(s => s.OfType<Word>().Count()).ToList().ForEach(s => Console.WriteLine("{0} {1}", s.OfType<Word>().Count(), s));

            // в вопросительных слова заданной длины без повторений
            var q = model.Where(s => s.ToString().EndsWith("?")).SelectMany(s => s.OfType<Word>()).Where(w => w.Length == 3)
                //.GroupBy(w => w.Value)
                //.Select(g => g.Key);
                .Select(w => new { Text = w.Value }) // компаратор автоматически создан
                .Distinct();            

            foreach (var w in q)
                Console.WriteLine(w.Text);            

            // замена слов заданной длины на подстроку (предложение)
            model.OrderBy(s => s.OfType<Word>().Count()).LastOrDefault().Replace(i => i.Value.Length == 5, sentence);
            model.OrderBy(s => s.OfType<Word>().Count()).ToList().ForEach(s => Console.WriteLine("{0} {1}", s.OfType<Word>().Count(), s));

            // удаление на согласную
            var words = model.SelectMany(s => s.OfType<Word>()) 
                .Where(w => w.Length == 4 && !w[0].Value[0].IsVowel())
                .ToArray();
            foreach (var w in words)
            {
                foreach (var s in model)
                    s.Remove(w);
            }

            model.OrderBy(s => s.OfType<Word>().Count()).ToList().ForEach(s => Console.WriteLine("{0} {1}", s.OfType<Word>().Count(), s));

            Console.ReadKey();
        }
    }
}