using System;
using System.IO;

namespace LemonOS
{
    public static partial class Commands
    {
        public static void Touch(string input, string[] args)
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

            string fileTo = string.Empty;

            if (input.Contains("\""))
                fileTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
            else
                fileTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();

            try
            {
                File.Create(fileTo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"touch: Failed to create file \"{fileTo}\": {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
