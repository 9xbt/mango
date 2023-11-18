using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PrismAPI.Hardware.GPU;
using mango.GUI;

namespace mango
{
    public class Kernel : Sys.Kernel
    {
        public const int Width = 800, Height = 600;

        public static Display Screen = Display.GetDisplay(Width, Height);
        public static SVGAIITerminal Console = new SVGAIITerminal(Width, Height, Resources.Font, FallbackTerminalUpdate);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "Version 1.2";
        public const string Copyright = "\nCopyright (c) 2023 Mobren\n";
        public static string Username;

        protected override void BeforeRun()
        {
            try
            {
                Screen.Update();
                Resources.Initialize();
                MouseDriver.Initialize();
                DiskManager.InitFS(FS);
                DiskManager.LoadSettings();
                WindowManager.Start();
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
                WindowManager.Update();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Unhandled exception in runtime: {ex.Message}");
            }
        }

        private static void FallbackTerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
        }
    }
}
