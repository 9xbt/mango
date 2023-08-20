using System;
using System.Text;
using System.IO;

namespace mango
{
    public static partial class Commands
    {
        public static void Echo(string input, string[] args)
        {
            if (args.Length > 2)
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
            }

            if (args.Length == 1)
            {
                Console.WriteLine(args[0], SVGAIIColor.Gray);
            }
            else if (args.Length == 2)
            {
                args[1] = args[1].Replace("/", "\\");

                try
                {
                    File.Create(@$"{Directory.GetCurrentDirectory()}\{args[1]}");
                    File.WriteAllText(@$"{Directory.GetCurrentDirectory()}\{args[1]}", args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"echo: Failed to write to file \"{args[1]}\": {ex.Message}", SVGAIIColor.Red);
                    return;
                }

                Console.WriteLine($"echo: Successfully written {Encoding.ASCII.GetBytes(args[0]).Length} bytes to \"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}\"", SVGAIIColor.Red);
            }
        }
    }
}
