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
                Kernel.DrawBootString("Initializing filesystem");
                VFSManager.RegisterVFS(fs);
                Directory.GetFiles(@"0:\");
                Directory.SetCurrentDirectory(@"0:\");
                //Logger.SuccessLog("Filesystem intialized.");
            }
            catch
            {
                Console.Clear();
                Logger.ErrorLog("Failed to initialize filesystem!");
            }
        }

        public static void LoadSettings()
        {
            Kernel.DrawBootString("Loading settings");

            try
            {
                Kernel.Username = File.ReadAllText(@"0:\mango\username.txt");
                //Logger.SuccessLog("Username loaded.");
                //Logger.InfoLog($"Switched username to {Kernel.Username}.");
            }
            catch
            {
                Logger.WarnLog("Failed to load username! Entering first time setup...");
                FirstTimeSetup();
            }

            try
            {
                string keymapTo = File.ReadAllText(@"0:\mango\keymap.txt");
                Commands.LoadKeys(new string[] { "loadkeys", keymapTo });
                //Logger.SuccessLog("Keymap set.");
                //Logger.InfoLog($"Set keymap to {keymapTo}.");
            }
            catch
            {
                Console.Clear();
                Logger.WarnLog("Failed to load keymap! Entering first time setup...");
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
