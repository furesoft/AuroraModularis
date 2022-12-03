using Actress;

namespace AuroraModularis.Messaging.Messages;

internal class ReturnMessage : Message
{
    public ReturnMessage(object value, IReplyChannel<object> channel) : base(value)
    {
        Channel = channel;
    }

    public IReplyChannel<object> Channel { get; set; }
}