using AuroraModularis.Messaging.Messages;
using System.Collections.Concurrent;

namespace AuroraModularis.Messaging;

public class Inbox
{
    private ConcurrentDictionary<string, ConcurrentBag<Action<object>>> handlers = new();
    private ConcurrentDictionary<string, Func<object, object>> returnHandlers = new();

    private object lockObject = new();

    internal Inbox(MessageBroker messageBroker)
    {
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

    public void Subscribe<T, U>(Func<T, U> callback)
    {
        string? fullName = typeof(T).FullName;

        lock (lockObject)
        {
            returnHandlers[fullName] = _ => callback((T)_);
        }
    }

    public void AddHandler<T, U>()
        where T : IMessageHandler<U>, new()
    {
        Subscribe<U>(new T().Subscribe);
    }

    public void AddHandler<T, U, V>()
        where T : IReturnMessageHandler<U, V>, new()
    {
        Subscribe<U, V>(new T().Subscribe);
    }

    internal bool InvokeIfPresent(Message message)
    {
        lock (lockObject)
        {
            if (message is ReturnMessage rmsg)
            {
                if (returnHandlers.TryGetValue(message.Value.GetType().FullName, out var retHandler))
                {
                    var returnValue = retHandler(message.Value);

                    rmsg.Channel.Reply(returnValue);
                    return true;
                }
            }

            if (handlers.TryGetValue(message.Value.GetType().FullName, out var value))
            {
                foreach (var handler in value)
                {
                    handler(message.Value);
                }
                return true;
            }

            return false;
        }
    }
}