using System;
using Cosmos.System;
using PrismAPI.Graphics;

namespace mango.GUI.Controls
{
    public class Button : Control
    {
        private static Color BorderColor = new Color(135, 135, 135);
        private static Color CornerColor = new Color(191, 191, 191);
        private static Color NormalColor = new Color(225, 225, 225);
        private static Color PressedColor = new Color(179, 179, 179);

        private Color BackgroundColor = NormalColor;
        private Color LastBackgroundColor = Color.Black;

        public string Text;
        public Action Clicked;

        public Button(Window Parent, int X, int Y, ushort Width, ushort Height, string Text, Action Clicked) : base(Parent, X, Y, Width, Height)
        {
            this.Text = Text;
            this.Clicked = Clicked;
            this.Parent.Controls.Add(this);

            Render();
        }

        public override void Render()
        {
            if (BackgroundColor != LastBackgroundColor)
            {
                Contents.Clear(BackgroundColor);

                Contents.DrawRectangle(0, 0, Convert.ToUInt16(Contents.Width - 1), Convert.ToUInt16(Contents.Height - 1), 0, BorderColor);
                Contents[0, 0] = CornerColor;
                Contents[0, Contents.Height - 1] = CornerColor;
                Contents[Contents.Width - 1, 0] = CornerColor;
                Contents[Contents.Width - 1, Contents.Height - 1] = CornerColor;

                Contents.DrawString((Contents.Width / 2) - (Resources.Font.MeasureString(Text) / 2), (Contents.Height / 2) - (Resources.Font.Size / 2), Text, Resources.Font, Color.Black);
            }
        }

        public override void Update()
        {
            if (Parent.Focused)
            {
                BackgroundColor = NormalColor;
                Render();

                if (IsMouseOver)
                {
                    MouseDriver.Mouse = Resources.Link;
                    MouseDriver.MouseOffsetY = 1;

                    if (MouseManager.LastMouseState == MouseState.None && MouseManager.MouseState == MouseState.Left)
                    {
                        BackgroundColor = Color.White;
                        Render();
                    }
                    else if (MouseManager.LastMouseState == MouseState.Left && MouseManager.MouseState == MouseState.None)
                    {
                        BackgroundColor = PressedColor;
                        Render();

                        Clicked?.Invoke();
                    }
                }
            }
        }
    }
}
