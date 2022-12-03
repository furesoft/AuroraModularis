using AuroraModularis.Messaging;

namespace AuroraModularis;

public class Outbox
{
    private readonly MessageBroker messageBroker;

    internal Outbox(MessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;
    }

    public void Post<T>(T message)
    {
        messageBroker.Post(message);
    }

    public U PostAndGet<T, U>(T message)
    {
        return messageBroker.PostAndGet<U>(message);
    }
}