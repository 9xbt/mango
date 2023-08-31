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

        public Dialogue(string Text, DialogueIcon Icon) : base(
            (WindowManager.Screen.Width / 2) - (((Icon != DialogueIcon.None ? 62 : 20) + MeasureLongestLine(Text)) / 2),
            (WindowManager.Screen.Height / 2) - (((Icon != DialogueIcon.None ? 62 : 20) + MeasureStringHeight(Text)) / 2),
            (Icon != DialogueIcon.None ? 62 : 20) + MeasureLongestLine(Text),
            (Icon != DialogueIcon.None ? 62 : 20) + MeasureStringHeight(Text))
        {
            try
            {
                this.Text = Text;
                this.Icon = Icon;

                OKButton = new Button(this, Width - 50, Height - 30, 40, 20, "OK", OKButton_Click);

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

        private static int MeasureStringHeight(string text)
        {
            return text.Split('\n').Length * Resources.Font.Size;
        }

        private static int MeasureLongestLine(string text)
        {
            int lastLength = 0;
            string longestLine = string.Empty;
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > lastLength)
                    longestLine = lines[i];

                lastLength = lines[i].Length;
            }

            return Resources.Font.MeasureString(longestLine);
        }
    }
}
