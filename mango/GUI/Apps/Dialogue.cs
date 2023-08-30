using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismAPI.Graphics;
using mango.GUI.Controls;

namespace mango.GUI.Apps
{
    public enum DialogueIcon
    {
        None = 0,
        Error = 2
    }

    public class Dialogue : Window
    {
        private Button OKButton;

        public DialogueIcon Icon;
        public string Text;

        public Dialogue(string Text, DialogueIcon Icon) : base((WindowManager.Screen.Width / 2) - (200 / 2), (WindowManager.Screen.Height / 2) - (150 / 2), 200, 150)
        {
            try
            {
                this.Text = string.Join('\n', Split(Text, Icon != DialogueIcon.None ? 17 : 22));
                this.Icon = Icon;

                OKButton = new Button(this, 150, 120, 40, 20, "OK", OKButton_Click);

                Render();
            }
            catch { }
        }

        public override void Render()
        {
            try
            {
                Contents.Clear(new Color(235, 235, 235));

                switch (Icon)
                {
                    case DialogueIcon.None:
                        break;

                    case DialogueIcon.Error:
                        Contents.DrawImage(10, 10, Resources.Error);
                        break;
                }

                Contents.DrawString(Icon != DialogueIcon.None ? 52 : 10, 10, Text, Resources.Font, Color.Black);

                base.Render();
            }
            catch { }
        }

        private void OKButton_Click()
        {
            try
            {
                WindowManager.RemoveWindow(this);
            }
            catch { }
        }

        private static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
