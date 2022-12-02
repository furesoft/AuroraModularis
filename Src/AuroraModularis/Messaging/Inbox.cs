namespace AuroraModularis.Messaging;

public class Inbox
{
    private readonly MessageBroker messageBroker;

    internal Inbox(MessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;
    }

    public void Subscribe<T>(Action<T> callback)
    {
        messageBroker.Subscribe<T>(_ => callback((T)_));
    }

    public void Subscribe<T, U>(Func<T, U> callback)
    {
        messageBroker.Subscribe(_ => callback((T)_));
    }

    public void AddHandler<T, U>()
        where T : IMessageHandler<U>, new()
    {
        Subscribe<U>(new T().Subscribe);
    }
}