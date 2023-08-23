using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using PrismAPI.Hardware.GPU;
using mango.GUI;
using mango.GUI.Apps;

namespace mango
{
    public class Kernel : Sys.Kernel
    {
        public static Display Screen = Display.GetDisplay(1280, 720);
        public static SVGAIITerminal Console = new SVGAIITerminal(1280, 720, Resources.Font, TerminalUpdate);
        public static CosmosVFS FS = new CosmosVFS();

        public const string Version = "Version 1.1";
        public const string Copyright = "\nCopyright (c) 2023 Mobren\n";

        public static string Username;

        protected override void BeforeRun()
        {
            try
            {
                Resources.Initialize();
                MouseDriver.Initialize();
                DiskManager.InitFS(FS);
                DiskManager.LoadSettings();
                Logger.InfoLog("Starting desktop enviorment...");

                var term = new Terminal();
                term.Console.WriteLine($"Welcome back, {Username}.\n");
                term.Console.DrawImage(Resources.Logo, false);
                term.Console.WriteLine($"\nThe mango Operating System\n{Version}{Copyright}");
                term.DrawPrompt();

                term.startX = term.Console.CursorX;
                term.startY = term.Console.CursorY;
                term.Action = TerminalAction.Shell;

                WindowManager.AddWindow(new Desktop());
                WindowManager.AddWindow(term);
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

        private static void TerminalUpdate()
        {
            Screen.DrawImage(0, 0, Console.Contents, false);
            Screen.Update();
        }
    }
}
