using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PrismAPI.Hardware.GPU;
using PrismAPI.Graphics;
using mango.GUI;

namespace mango
{
    public class Kernel : Sys.Kernel
    {
        public static Display Screen = Display.GetDisplay(1280, 720);
        public static SVGAIITerminal Console = new SVGAIITerminal(1280, 720, Resources.Font, FallbackTerminalUpdate);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "Version 1.1";
        public const string Copyright = "\nCopyright (c) 2023 Mobren\n";
        public static string Username;

        protected override void BeforeRun()
        {
            try
            {
                FallbackTerminalUpdate();
                BootAnimation();
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

        private void BootAnimation()
        {
            for (int i = 50; i <= 228; i++)
            {
                Screen.Clear();
                Screen.DrawImage((Screen.Width / 2) - (Resources.Logo.Width / 2), i, Resources.Logo, false);
                Screen.Update();
            }
        }

        private static void FallbackTerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
        }

        public static void DrawBootString(string text)
        {
            Screen.DrawFilledRectangle(0, 308, Screen.Width, 16, 0, Color.Black);
            Screen.DrawString((Screen.Width / 2) - (Resources.Font.MeasureString(text) / 2), 308, text, Resources.Font, Color.White);
            Screen.Update();
        }
    }
}
