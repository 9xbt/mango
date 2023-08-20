using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.Core.Memory;
using PrismAPI.Hardware.GPU;

namespace mango
{
    public class Kernel : Sys.Kernel
    {
        public static Display Screen = Display.GetDisplay(1280, 720);
        public static SVGAIITerminal Console = new SVGAIITerminal(1280, 720, Resources.Font, TerminalUpdate);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "Version 1.0";
        public const string Copyright = "\nCopyright (c) 2023 Mobren\n";

        public static string Username;

        protected override void BeforeRun()
        {
            try
            {
                DiskManager.InitFS(FS);
                DiskManager.LoadSettings();

                Console.Clear();
                Console.WriteLine($"Welcome back, {Username}.\n");
                Console.DrawImage(Resources.Logo, false);
                Console.WriteLine($"\nThe mango Operating System\n{Version}{Copyright}");
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Unhandled exception in bootstrap: {ex.Message}");
            }
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

        public static void TerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
            Heap.Collect();
        }
    }
}
