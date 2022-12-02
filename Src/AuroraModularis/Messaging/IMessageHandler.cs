namespace AuroraModularis.Messaging;

public interface IMessageHandler<T>
{
    void Subscribe<T>(T message);
}