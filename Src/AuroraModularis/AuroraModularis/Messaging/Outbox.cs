namespace AuroraModularis;

public class Outbox
{
    internal Outbox()
    {
    }

    public void Broadcast<T>(T message)
    {
    }

    public void Post<T>(T message)
    {
    }

    public Task<U> PostAndGet<T, U>(T message)
    {
        return null;
    }
}