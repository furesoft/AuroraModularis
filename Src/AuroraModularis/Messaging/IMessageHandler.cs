namespace AuroraModularis.Messaging;

public interface IMessageHandler<in T>
{
    void Subscribe(T message);
}