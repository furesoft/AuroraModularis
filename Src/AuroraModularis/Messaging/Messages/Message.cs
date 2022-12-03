namespace AuroraModularis.Messaging.Messages;

internal class Message
{
    public Message(object value)
    {
        Value = value;
    }

    public object Value { get; set; }
    public bool Repost { get; set; }
}