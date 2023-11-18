using Cosmos.System;

namespace mango
{
    public static partial class Commands
    {
        public static void Shutdown()
        {
            Console.WriteLine("shutdown: Shutting down...");
            Power.Shutdown();
        }
    }
}
