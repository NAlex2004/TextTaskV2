namespace NAlex.TextModel.Interfaces
{
    public interface ICorcordanceBuilder<TKey, TEntry>
    {
        ICorcordance<TKey, TEntry> BuildCorcordance ();
    }
}