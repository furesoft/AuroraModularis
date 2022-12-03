namespace AuroraModularis.Messaging;

public interface IReturnMessageHandler<in T, out U>
{
    U Subscribe(T message);
}