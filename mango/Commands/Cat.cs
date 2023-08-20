using System;
using System.IO;

namespace mango
{
    public static partial class Commands
    {
        public static void Cat(string input, string[] args)
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

            args[1] = args[1].Replace("/", "\\");

            string catTo = string.Empty;

            if (input.Contains("\""))
                catTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(5, input.Length - 5)}".Trim();
            else
                catTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();

            try
            {
                Console.WriteLine(File.ReadAllText(catTo), SVGAIIColor.Gray);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"cat: Failed to read file: {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
