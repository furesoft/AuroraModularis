using AuroraModularis;

namespace TestConsole
{
    internal class Program
    {
        private static Task Main(string[] args)
        {
            return Bootstrapper.RunAsync(Environment.CurrentDirectory);

            while (true)
            {
            }
        }
    }
}