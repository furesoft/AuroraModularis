using AuroraModularis.Messaging;

namespace AuroraModularis;

public class Outbox
{
    private readonly MessageBroker messageBroker;

    internal Outbox(MessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;
    }

    public void Post<T>(T message, bool repost = false)
    {
        messageBroker.Post(message, repost);
    }

    public U PostAndGet<T, U>(T message, bool repost = false)
    {
        return messageBroker.PostAndGet<U>(message, repost);
    }
}