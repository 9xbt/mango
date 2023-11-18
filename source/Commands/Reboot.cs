using Cosmos.System;

namespace mango
{
    public static partial class Commands
    {
        public static void Reboot()
        {
            Console.WriteLine("reboot: Rebooting...");
            Power.Reboot();
        }
    }
}
