using Cosmos.System;

namespace mango.GUI
{
    public static class MouseDriver
    {
        public static void Initialize()
        {
            MouseManager.ScreenWidth = WindowManager.Screen.Width;
            MouseManager.ScreenHeight = WindowManager.Screen.Height;
            Logger.SuccessLog("Mouse driver initialized.");
        }

        public static void Update()
        {
            WindowManager.Screen.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Resources.Mouse, true);
        }
    }
}
