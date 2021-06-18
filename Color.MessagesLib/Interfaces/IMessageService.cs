namespace Color.MessageLib.Interfaces
{
    public interface IMessageService
    {
        void Init();
        bool Enqueue(string message);
    }
}
