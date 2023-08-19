using System;
using System.IO;

namespace LemonOS
{
    public static partial class Commands
    {
        public static void SU(string input, string[] args)
        {
            if (!input.Contains('"') && args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (!input.Contains('"') && args.Length > 2)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            string userTo = string.Empty;

            if (input.Contains("\""))
                userTo = input.Substring(4, input.Length - 5).Trim();
            else
                userTo = args[1].Trim();

            try
            {
                Directory.CreateDirectory(@"0:\LunarOS");
                File.WriteAllText(@"0:\LunarOS\username.txt", userTo);
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
