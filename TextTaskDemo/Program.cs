using System;
using System.IO;
using System.Linq;
using NAlex.TextModel.Extensions;
using NAlex.TextModel.Factories;
using NAlex.TextModel.Interfaces;
using NAlex.TextModel.Model;
using NAlex.TextModel.Parsers;
using System.Collections.Generic;
using System.Configuration;

namespace TextTaskDemo
{
	class MainClass
	{
		static void WriteTextModel(IEnumerable<ISentence> model)
		{
			Console.WriteLine(" Модель текста:\n");
			foreach (ISentence sentence in model)
				Console.WriteLine(sentence);
			Console.WriteLine();
		}

		static void WriteOrderedSentencies(IEnumerable<ISentence> model)
		{
			Console.WriteLine(" Предложения по возрастанию количества слов:\n");
			foreach (var sentence in model.OrderBy(s => s.OfType<IWord>().Count()))
			{
				Console.WriteLine("{0, -5}{1}", sentence.OfType<IWord>().Count(), sentence);
			}
			Console.WriteLine();
		}

		static void WriteWordsOfInterrogativeSentence(IEnumerable<ISentence> model, int wordLength)
		{
			Console.WriteLine(" Слова длиной {0} символов в вопросительных предложениях без повторений:\n", wordLength);
			var sentencies = model.Where(s => s.ToString().Contains("?"));

			foreach (var sentence in sentencies)
			{
				var words = sentence.OfType<IWord>().Where(w => w.Length == wordLength).Select(w => w.Value.ToLower()).Distinct();
				if (words.Any())
				{
					Console.WriteLine(sentence);
					Console.Write("Слова:  ");
					foreach (var word in words)
					{
						Console.Write("{0} ", word);
					}
					Console.WriteLine();
				}
			}

			Console.WriteLine();
		}

		static void DeleteWordsStartWithConsonant(IEnumerable<ISentence> model, int wordLength)
		{
			Console.WriteLine("Удаляем слова длиной {0} символов, начинающиеся на согласную:", wordLength);
			Console.Write(" Слова: ");
			var words = model.SelectMany(s => s.OfType<IWord>())
				.Where(w => w.Length == wordLength && !w[0].Value[0].IsVowel())
				.ToArray();
			foreach (var w in words)
			{
				Console.Write("{0} ", w);
				foreach (var s in model)
					s.Remove(w);
			}
			 
			Console.WriteLine();
			Console.WriteLine("Не удаляем:");
			var notDeleted = model.SelectMany(s => s.OfType<IWord>())
				.Where(w => w.Length == wordLength && w[0].Value[0].IsVowel())
				.ToArray();
			foreach (var w in notDeleted)
			{
				Console.Write("{0} ", w);

			}

			Console.WriteLine();
			Console.WriteLine();
		}

		static void ReplaceWordInSentence(IEnumerable<ISentence> model, int index, int wordLength,
			IEnumerable<ITextItem> newItems)
		{
			Console.WriteLine("Замена слов длиной {0} символов в предложении на указанные:", wordLength);
			var qq = model.ElementAtOrDefault(index);
			if (qq != null)
				qq.Replace(i => i.Value.Length == wordLength, newItems);
		}

		static void WriteSentence(IEnumerable<ISentence> model, int index)
		{
			var sentence = model.ElementAtOrDefault(index);
			if (sentence != null)
				Console.WriteLine(sentence);
		}

		static void CreateCorcordance(string inputFile, string outputFile, int linesPerPage)
		{			
			ICorcordanceBuilder<WordSymbol, IEntry<IWord, int>> corcordanceBuilder = new CorcordanceFileBuilder(inputFile,
				linesPerPage,
				new PageParser(), new EmptySentenceFactory());

			var corcordance = corcordanceBuilder.BuildCorcordance();

			using (FileStream oStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
			{
				corcordance.ToStream().CopyTo(oStream);
				oStream.Flush();
			}
		}

		static void PagesToFile(string inputFile, string outputFile, int linesPerPage)
		{
			IPageParser parser = new PageParser();
			using (FileStream inputStream = new FileStream(inputFile, FileMode.Open))
			{
				var model = parser.GetTextPages(inputStream, linesPerPage, new EmptySentenceFactory());

				using (StreamWriter writer = new StreamWriter(outputFile))
				{
					foreach (var page in model)
					{
						writer.WriteLine(page);
					}
					writer.Flush();
				}
			}
		}

		public static void Main(string[] args)
		{
			string path = ConfigurationManager.AppSettings["SourceDir"] + Path.DirectorySeparatorChar;
			string inputPath = path + "text.txt";

			PagesToFile(inputPath, path + "pages.txt", 5);
			CreateCorcordance(inputPath, path + "corcordance.txt", 5);

			IEnumerable<ISentence> model;

			using (FileStream fStream = new FileStream(inputPath, FileMode.Open))
			{
				IPageParser parser = new PageParser();
				var pages = parser.GetTextPages(fStream, 10000000, new EmptySentenceFactory());
				model = pages.SelectMany(p => p.SelectMany(s => s));
			}

			WriteTextModel(model);

			Console.WriteLine("---------------------------------------------------");
			WriteOrderedSentencies(model);

			Console.WriteLine("---------------------------------------------------");
			WriteWordsOfInterrogativeSentence(model, 6);

			Console.WriteLine("---------------------------------------------------");
			DeleteWordsStartWithConsonant(model, 9);
			WriteTextModel(model);

			Console.WriteLine("---------------------------------------------------");
			WriteSentence(model, 11);
			ReplaceWordInSentence(model, 11, 4, new IWord[] {new Word("REPLACED")});
			WriteSentence(model, 11);


//			Console.ReadKey ();
		}
	}
}
