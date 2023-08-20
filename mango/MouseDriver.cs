using Cosmos.System;

namespace mango
{
    public static class MouseDriver
    {
        public static void Initialize()
        {
            MouseManager.ScreenWidth = Kernel.Screen.Width;
            MouseManager.ScreenHeight = Kernel.Screen.Height;
            MouseManager.X = 0;
            MouseManager.Y = 0;
            Logger.SuccessLog("Mouse driver initialized.");
        }

        public static void Update()
        {
            Kernel.Screen.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Resources.Mouse, true);
        }
    }
}
