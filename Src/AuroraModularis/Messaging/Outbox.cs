using AuroraModularis.Messaging;

namespace AuroraModularis;

public class Outbox
{
    private readonly MessageBroker messageBroker;

    internal Outbox(MessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;
    }

    public void Broadcast<T>(T message)
    {
        messageBroker.Broadcast(message);
    }

    public void Post<T>(T message)
    {
        messageBroker.Post(message);
    }

    public async Task<U> PostAndGet<T, U>(T message)
    {
        var value = await messageBroker.PostAndGet(message);

        return (U)value;
    }
}