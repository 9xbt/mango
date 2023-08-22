using System;
using System.IO;

namespace mango
{
    public static partial class Commands
    {
        public static void SU(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (args.Length > 2)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            string userTo = userTo = args[1].Trim();

            try
            {
                Directory.CreateDirectory(@"0:\mango");
                File.WriteAllText(@"0:\mango\username.txt", userTo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"su: Failed to save username: {ex.Message}", SVGAIIColor.Red);
                return;
            }

            Kernel.Username = userTo;

            Console.WriteLine($"su: Switched to {userTo}", SVGAIIColor.Gray);
        }
    }
}
