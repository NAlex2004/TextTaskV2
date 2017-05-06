using NAlex.TextModel.Interfaces;

namespace NAlex.TextModel.Model
{
    public class CorcordanceFileBuilder: ICorcordanceBuilder<WordSymbol, IEntry<IWord, int>>
    {
        public CorcordanceFileBuilder(string inputFile)
        {

        }

        public ICorcordance<WordSymbol, IEntry<IWord, int>> BuildCorcordance()
        {
            throw new System.NotImplementedException();
        }
    }
}