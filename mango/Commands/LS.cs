using System.IO;

namespace mango
{
    public static partial class Commands
    {
        public static void LS()
        {
            if (Directory.GetDirectories(Directory.GetCurrentDirectory()).Length > 0)
                Console.Write(string.Join("  ", Directory.GetDirectories(Directory.GetCurrentDirectory())).Trim() + "  ", SVGAIIColor.Blue);
            if (Directory.GetFiles(Directory.GetCurrentDirectory()).Length > 0)
                Console.Write(string.Join("  ", Directory.GetFiles(Directory.GetCurrentDirectory())).Trim(), SVGAIIColor.Red);
            Console.WriteLine();
        }
    }
}
