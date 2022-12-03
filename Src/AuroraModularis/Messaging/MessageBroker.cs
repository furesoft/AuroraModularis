using Actress;
using AuroraModularis.Messaging.Messages;
using System.Collections.Concurrent;

namespace AuroraModularis.Messaging;

internal class MessageBroker
{
    private ConcurrentBag<Inbox> inboxes = new();
    private MailboxProcessor<Message> mailboxProcessor;

    public void Start()
    {
        mailboxProcessor = MailboxProcessor.Start<Message>(async _ =>
        {
            while (true)
            {
                var message = await _.Receive();
                foreach (var inbox in inboxes)
                {
                    inbox.InvokeIfPresent(message);
                }
            }
        });
        //    mailboxProcessor.Errors.Subscribe(OnError);
    }

    internal void AddInbox(Inbox inbox)
    {
        inboxes.Add(inbox);
    }

    internal void Post(object message)
    {
        mailboxProcessor.Post(new Message(message));
    }

    internal T PostAndGet<T>(object message)
    {
        return (T)mailboxProcessor.PostAndReply<object>(
            channel => new ReturnMessage(message, channel));
    }

    private void OnError(Exception error)
    {
        Post(error);
    }
}