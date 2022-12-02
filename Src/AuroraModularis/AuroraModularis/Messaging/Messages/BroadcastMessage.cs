namespace AuroraModularis.Messaging.Messages;

internal class BroadcastMessage : IMessage
{
    public object Value { get; set; }
}