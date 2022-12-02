using AuroraModularis;

namespace TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrapper.Run(Environment.CurrentDirectory);
        }
    }
}