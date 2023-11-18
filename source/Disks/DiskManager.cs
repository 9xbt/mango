using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace mango
{
    public static class DiskManager
    {
        private static SVGAIITerminal Console = Kernel.Console;

        public static void InitFS(CosmosVFS fs)
        {
            try
            {
                VFSManager.RegisterVFS(fs);
                Directory.GetFiles(@"0:\");
                Directory.SetCurrentDirectory(@"0:\");

                Logger.SuccessLog("Filesystem intialized.");
            }
            catch
            {
                Logger.ErrorLog("Failed to initialize filesystem!");
            }
        }

        public static void LoadSettings()
        {
            try
            {
                Kernel.Username = File.ReadAllText(@"0:\mango\username.txt");
                Logger.SuccessLog("Username loaded.");

                Commands.LoadKeys(new string[] { "loadkeys", File.ReadAllText(@"0:\mango\keymap.txt") });
                Logger.SuccessLog("Keymap set.");
            }
            catch
            {
                Logger.WarnLog("Failed to load settings! Entering first time setup...");
                FirstTimeSetup();
            }
        }

        private static void FirstTimeSetup()
        {
            Console.WriteLine("\nWelcome to the mango operating system, newbie!\n");
            Console.DrawImage(Resources.Logo, false);
            Console.WriteLine("\nWe are going to get you up and running by asking you a few questions:\n");

            Console.Write("- What username do you want to use?: ");
            Commands.SU(new string[] { "su", Console.ReadLine() });
            Commands.LoadKeys(new string[] { "loadkeys", "us-us" });

            Console.WriteLine("\nThanks for setting up mango!\n");

            Logger.InfoLog("Press any key to continue...");
            Console.ReadKey(true);
        }

        public static string GetCosmosLikePath(string path)
        {
            return @"0:\" + path.Substring(1).Replace("/", "\\");
        }

        public static string GetUnixLikePath(string path)
        {
            return path.Replace("\\", "/").Replace("0:/", "/");
        }
    }
}
