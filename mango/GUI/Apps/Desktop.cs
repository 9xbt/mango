using System;
using Cosmos.HAL;
using PrismAPI.Graphics;

namespace mango.GUI.Apps
{
    public class Desktop : Window
    {
        private byte lastMinute = RTC.Minute;

        public Desktop() : base(0, 0, WindowManager.Screen.Width, WindowManager.Screen.Height, "WM.Desktop")
        {
            Movable = false;
        }

        public override void Render()
        {
            string timeString = DateTime.Now.ToString("t");

            Contents.DrawFilledRectangle(0, 0, Contents.Width, 20, 0, Color.LightBlack);
            Contents.DrawString(2, 2, "1", Resources.Font, Color.White);
            if (WindowManager.FocusedWindow != null)
                if (!WindowManager.FocusedWindow.Name.StartsWith("WM."))
                    Contents.DrawString(18, 2, WindowManager.FocusedWindow.Name, Resources.Font, BorderColor2);
            Contents.DrawString(Contents.Width - Resources.Font.MeasureString(timeString) - 2, 2, timeString, Resources.Font, BorderColor2);
            Contents.DrawImage(0, 20, Resources.Background, false);
        }

        public override void Update()
        {
            if (RTC.Minute != lastMinute || WindowManager.FocusedWindow.Name != WindowManager.LastFocusedWindow.Name)
            {
                Render();
                lastMinute = RTC.Minute;
            }
        }
    }
}
