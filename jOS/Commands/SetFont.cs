using System;
using System.IO;

namespace mango
{
    public static partial class Commands
    {
        public static void SetFont(string input, string[] args)
        {
            Console.WriteLine($"setfont: warning: setfont is untested, use at your own risk", SVGAIIColor.Gray);

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

            string fontTo = string.Empty;

            if (input.Contains("\""))
                fontTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
            else
                fontTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();

            try
            {
                Resources.rawFont = File.ReadAllBytes(fontTo);
                Resources.GenerateFont();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"setfont: Failed to set font \"{fontTo}\": {ex.Message}", SVGAIIColor.Red);
                return;
            }
        }
    }
}
