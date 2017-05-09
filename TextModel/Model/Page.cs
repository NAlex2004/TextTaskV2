using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class Page: ITextPage
    {
        private IList<ICollection<ISentence>> lines = new List<ICollection<ISentence>>();

        public int PageNumber
        {
            get;
            protected set;
        }

        public int LinesPerPage
        {
            get;
            private set;
        }

        public Page(int linesPerPage, int pageNumber = 0)
        {
            LinesPerPage = linesPerPage;
            PageNumber = pageNumber;
        }

        public IEnumerator<ICollection<ISentence>> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public ICollection<ISentence> this[int index]
        {
            get
            {
                return lines[index];
            }
            set
            {
                lines[index] = value;
            }
        }

        public void Add(ICollection<ISentence> item)
        {
            if (item != null)
                lines.Add(item);
        }

        public void Clear()
        {
            lines.Clear();
        }       

        public int Count
        {
            get { return lines.Count; }
        }        

        public bool Remove(ICollection<ISentence> item)
        {
            return item != null && lines.Remove(item);
        }


        public void RemoveAt(int index)
        {
            lines.RemoveAt(index);
        }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format(" Page # {0}", PageNumber));
			sb.AppendLine();
			foreach (var sentencies in lines)
			{
				foreach (var sentence in sentencies)
					sb.Append(sentence);
				sb.AppendLine();
			}
			sb.AppendLine();

			return sb.ToString();
		}
    }
}
