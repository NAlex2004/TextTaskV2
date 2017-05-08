using System;
using System.IO;
using System.Linq;
using NAlex.TextModel.Extensions;
using NAlex.TextModel.Factories;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;
using NAlex.TextModel.Parsers;
using System.Collections.Generic;

namespace TextTaskDemo
{
	class MainClass
	{
		static void WriteTextModel(IEnumerable<ISentence> model)
		{
			Console.WriteLine (" Модель текста:\n");
			foreach(ISentence sentence in model)
				Console.WriteLine (sentence);
			Console.WriteLine ();
		}

		static void WriteOrderedSentencies(IEnumerable<ISentence> model)
		{
			Console.WriteLine (" Предложения по возрастанию количества слов:\n");
			foreach(var sentence in model.OrderBy (s => s.OfType<IWord> ().Count ()))
			{
				Console.WriteLine ("{0, -5}{1}", sentence.OfType<IWord> ().Count (), sentence);
			}
			Console.WriteLine ();
		}

		public static void Main (string[] args)
		{
			using (FileStream fStream = new FileStream("test.txt", FileMode.Open))
			{				
				IPageParser parser = new PageParser ();
				var pages = parser.GetTextPages (fStream, 10000000, new EmptySentenceFactory());
				IEnumerable<ISentence> model = pages.SelectMany (p => p.SelectMany (s => s));
				WriteTextModel (model);
				Console.WriteLine ("---------------------------------------------------");
				WriteOrderedSentencies (model);
			}
			Console.ReadKey ();
		}
	}
}
