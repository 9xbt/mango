using System;
using System.IO;

namespace LemonOS
{
    public static partial class Commands
    {
        public static void RmDir(string input, string[] args)
        {
            if (!input.Contains('"') && args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (!input.Contains('"') && args.Length > 3)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            string dirTo = string.Empty;

            if (input.Contains("\""))
                dirTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(7, input.Length - 8)}".Trim();
            else
                dirTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();

            try
            {
                Directory.Delete(dirTo, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"rmdir: Failed to delete directory \"{dirTo}\": {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
