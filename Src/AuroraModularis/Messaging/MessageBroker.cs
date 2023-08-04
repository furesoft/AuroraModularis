using Actress;
using AuroraModularis.Messaging.Messages;
using System.Collections.Concurrent;

namespace AuroraModularis.Messaging;

public class MessageBroker
{
    private readonly ConcurrentBag<Inbox> _inboxes = new();
    private MailboxProcessor<Message> mailboxProcessor;

    public IReadOnlyCollection<Inbox> GetInboxes() => _inboxes;

    public void Start()
    {
        mailboxProcessor = MailboxProcessor.Start<Message>(ProcessMessages);
        mailboxProcessor.Errors.Subscribe(OnError);
    }

    private async Task ProcessMessages(MailboxProcessor<Message> _)
    {
        while (true)
        {
            var message = await _.Receive();

            bool hasBeenInvoked = false;
            foreach (var inbox in _inboxes)
            {
                hasBeenInvoked = inbox.InvokeIfPresent(message);
            }

            if (!hasBeenInvoked && message.Repost)
            {
                mailboxProcessor.Post(message);
            }
        }
    }

    internal void AddInbox(Inbox inbox)
    {
        _inboxes.Add(inbox);
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