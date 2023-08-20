using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;
using PrismAPI.Graphics.Fonts;
using PrismAPI.Hardware.GPU;

namespace LemonOS
{
    public class Kernel : Sys.Kernel
    {
        [ManifestResourceStream(ResourceName = "LemonOS.Resources.Font.btf")] private static byte[] rawFont;
        public static Font Font = new Font(rawFont, 16);

        public static Display Screen = Display.GetDisplay(1920, 1080);
        public static SVGAIITerminal Console = new SVGAIITerminal(1920, 1080, Font, Update);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "Version 1.0";
        public const string Copyright = "\nCopyright (c) 2023 Mobren\n";
        public const string Logo = " _                                ___  ____\n| |    ___ _ __ ___   ___  _ __  / _ \\/ ___|\n| |   / _ \\ '_ ` _ \\ / _ \\| '_ \\| | | \\___ \\\n| |__|  __/ | | | | | (_) | | | | |_| |___) |\n|_____\\___|_| |_| |_|\\___/|_| |_|\\___/|____/ \n";

        public static string Username;

        protected override void BeforeRun()
        {
            DiskManager.InitFS(FS);
            DiskManager.LoadSettings();

            Console.Clear();
            Console.WriteLine($"Welcome back, {Username}.");
            Console.WriteLine(Logo, SVGAIIColor.Yellow);
            Console.WriteLine(Version + Copyright);
        }

        protected override void Run()
        {
            try
            {
                Shell.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception in runtime: {ex.Message}", SVGAIIColor.Red);
            }
        }

        private static void Update()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
            Heap.Collect();
        }
    }
}
