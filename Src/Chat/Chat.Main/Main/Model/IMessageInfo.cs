namespace Chat.Main.Model
{
    public interface IMessageInfo
    {
        long AuthorId { get; }

        string Text { get; }

        long[] CategoryIds { get; }
    }
}