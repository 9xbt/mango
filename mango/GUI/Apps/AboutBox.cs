using PrismAPI.Graphics;

namespace mango.GUI.Apps
{
    public class AboutBox : Window
    {
        private bool animation = true;

        public AboutBox() : base((WindowManager.Screen.Width / 2) - (220 / 2), -64, 220, 64, "About mwm") { }

        public override void Render()
        {
            string[] texts = new string[]
            {
                "mwm - mango window manager",
                WindowManager.Version,
                Kernel.Copyright.Replace("\n", "\0")
            };

            Contents.Clear(Color.LightBlack);
            base.Render();
            Contents.DrawString((Contents.Width / 2) - (Resources.Font.MeasureString(texts[0]) / 2), 4, texts[0], Resources.Font, Color.White);
            Contents.DrawString((Contents.Width / 2) - (Resources.Font.MeasureString(texts[1]) / 2), 24, texts[1], Resources.Font, Color.White);
            Contents.DrawString((Contents.Width / 2) - (Resources.Font.MeasureString(texts[2]) / 2), 44, texts[2], Resources.Font, Color.White);
        }

        public override void Update()
        {
            if (animation)
            {
                if (Y >= 30)
                {
                    animation = false;
                }

                Y += 1;
            }
        }
    }
}
