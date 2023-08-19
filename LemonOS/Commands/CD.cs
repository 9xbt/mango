using System.IO;

namespace LemonOS
{
    public static partial class Commands
    {
        public static void CD(string input, string[] args)
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

            string cdTo = string.Empty;

            if (args[1] == "..")
            {
                try
                {
                    cdTo = Directory.GetCurrentDirectory().TrimEnd('\\').Remove(Directory.GetCurrentDirectory().LastIndexOf('\\') + 1);
                    cdTo = cdTo.Remove(cdTo.Length - 1);
                }
                catch { }

                if (!cdTo.StartsWith(@"0:\"))
                    cdTo = @"0:\"; // Directory error correction
            }
            else
            {
                if (input.Contains("\""))
                    cdTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{input.Substring(4, input.Length - 5)}".Trim();
                else
                    cdTo = @$"{Directory.GetCurrentDirectory()}{(Directory.GetCurrentDirectory() != @"0:\" ? @"\" : "")}{args[1]}".Trim();
            }

            if (Directory.Exists(cdTo))
                Directory.SetCurrentDirectory(cdTo);
            else
                Console.WriteLine($"cd: No such directory \"{cdTo.Substring(cdTo.LastIndexOf("\\") + 1)}\"", SVGAIIColor.Red);
        }
    }
}
