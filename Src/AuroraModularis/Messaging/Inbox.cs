using System.Collections.Concurrent;

namespace AuroraModularis.Messaging;

public class Inbox
{
    private readonly MessageBroker messageBroker;

    private ConcurrentDictionary<string, ConcurrentBag<Action<object>>> handlers = new();

    private object lockObject = new();

    internal Inbox(MessageBroker messageBroker)
    {
        this.messageBroker = messageBroker;

        messageBroker.AddInbox(this);
    }

    public void Subscribe<T>(Action<T> callback)
    {
        string? fullName = typeof(T).FullName;

        lock (lockObject)
        {
            if (!handlers.ContainsKey(fullName))
            {
                handlers.GetOrAdd(fullName, (_) => new());
            }

            handlers[fullName].Add(_ => callback((T)_));
        }
    }

    public void AddHandler<T, U>()
        where T : IMessageHandler<U>, new()
    {
        Subscribe<U>(new T().Subscribe);
    }

    internal void InvokeIfPresent(object message)
    {
        InvokeIfPresent(message.GetType().FullName, message);
    }

    internal void InvokeIfPresent(string messageType, object message)
    {
        lock (lockObject)
        {
            if (handlers.TryGetValue(messageType, out var value))
            {
                foreach (var handler in value)
                {
                    handler(message);
                }
            }
        }
    }
}