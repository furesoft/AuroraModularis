namespace AuroraModularis.Messaging;

internal class MessageBroker
{
    internal void Broadcast(object message)
    {
        throw new NotImplementedException();
    }

    internal void Post(object message)
    {
        throw new NotImplementedException();
    }

    internal Task<object> PostAndGet(object message)
    {
        throw new NotImplementedException();
    }

    internal void RegisterInbox(Inbox inbox)
    {
        throw new NotImplementedException();
    }

    internal void Subscribe<T>(Func<object, T> value)
    {
        throw new NotImplementedException();
    }

    internal void Subscribe<T>(Action<object> callback)
    {
    }
}