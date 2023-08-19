using System;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace LemonOS
{
    public static class DiskManager
    {
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
                Kernel.Username = File.ReadAllText(@"0:\LunarOS\username.txt");
                Logger.SuccessLog("Username loaded.");
            }
            catch
            {
                Logger.WarnLog("Failed to load username, switching to root...");
                Kernel.Username = "root";
            }
        }
    }
}
