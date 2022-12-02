using Actress;
using System.Collections.Concurrent;

namespace AuroraModularis.Messaging;

internal class MessageBroker
{
    private ConcurrentBag<Inbox> inboxes = new();
    private MailboxProcessor<object> mailboxProcessor;

    public void Start()
    {
        mailboxProcessor = MailboxProcessor.Start<object>(async _ =>
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
        mailboxProcessor.Post(message);
    }

    internal Task<object> PostAndGet(object message)
    {
        throw new NotImplementedException();
    }

    private void OnError(Exception error)
    {
        Post(error);
    }
}