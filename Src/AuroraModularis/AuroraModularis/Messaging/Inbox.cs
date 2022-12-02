namespace AuroraModularis.Messaging;

public class Inbox
{
    internal Inbox()
    {

    }

    public void Subscribe<T>(Action<T> callback)
    {

    }

    public U Subscribe<T, U>(Func<T, U> callback)
    {
        return default;
    }
}