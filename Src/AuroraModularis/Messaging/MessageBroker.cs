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

                bool hasBeenInvoked = false;
                foreach (var inbox in inboxes)
                {
                    hasBeenInvoked = inbox.InvokeIfPresent(message);
                }

                if (!hasBeenInvoked && message.Repost)
                {
                    mailboxProcessor.Post(message);
                }
            }
        });
        //    mailboxProcessor.Errors.Subscribe(OnError);
    }

    internal void AddInbox(Inbox inbox)
    {
        inboxes.Add(inbox);
    }

    internal void Post(object message, bool repost)
    {
        mailboxProcessor.Post(new Message(message) { Repost = repost });
    }

    internal T PostAndGet<T>(object message, bool repost)
    {
        return (T)mailboxProcessor.PostAndReply<object>(
            channel => new ReturnMessage(message, channel) { Repost = repost });
    }

    private void OnError(Exception error)
    {
        Post(error, false);
    }
}