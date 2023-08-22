using System;
using Cosmos.System;
using PrismAPI.Graphics;

namespace mango.GUI
{
    public class Window
    {
        private long dsx, dsy;
        private uint dsmx, dsmy;
        private bool d = false;

        protected static Color BorderColor1 = new Color(150, 150, 150);
        protected static Color BorderColor2 = new Color(200, 200, 200);

        public long X, Y;
        public ushort Width, Height;
        public string Name;
        public bool Movable = true, Resizable = true;

        public bool Focused
        {
            get
            {
                return WindowManager.FocusedWindow == this;
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height;
            }
        }

        public int GetMouseCorner()
        {
            if (MouseManager.X > X - 5 && MouseManager.X < X && MouseManager.Y > Y - 5 && MouseManager.Y < Y)
                return 0;
            else if (MouseManager.X > X + Width && MouseManager.X < X + Width + 5 && MouseManager.Y > Y - 5 && MouseManager.Y < Y)
                return 1;
            else if (MouseManager.X > X - 5 && MouseManager.X < X && MouseManager.Y > Y + Height && MouseManager.Y < Y + Height + 5)
                return 2;
            else if (MouseManager.X > X + Width && MouseManager.X < X + Width + 5 && MouseManager.Y > Y + Height && MouseManager.Y < Y + Height + 5)
                return 3;
            else
                return -1;
        }

        public Canvas Contents;

        public Window(int X, int Y, int Width, int Height, string Name)
        {
            this.X = X;
            this.Y = Y;
            this.Width = (ushort)Width;
            this.Height = (ushort)Height;
            this.Name = Name;

            Contents = new Canvas(this.Width, this.Height);

            Render();
        }

        public virtual void Render()
        {
            Contents.DrawRectangle(1, 1, Convert.ToUInt16(Contents.Width - 3), Convert.ToUInt16(Contents.Height - 3), 0, BorderColor1);
            Contents.DrawRectangle(0, 0, Convert.ToUInt16(Contents.Width - 1), Convert.ToUInt16(Contents.Height - 1), 0, BorderColor2);
            Contents[Contents.Width - 2, Contents.Height - 2] = BorderColor1;
            Contents[Contents.Width - 1, Contents.Height - 1] = BorderColor2;
        }

        public virtual void Update()
        {
            if (IsMouseOver && MouseManager.LastMouseState == MouseState.Left && MouseManager.MouseState == MouseState.None)
            {
                WindowManager.MoveWindowToFront(this);
                MouseManager.LastMouseState = MouseState.None;
            }

            if (Movable && IsMouseOver && Focused && MouseManager.LastMouseState == MouseState.None && MouseManager.MouseState == MouseState.Left)
            {
                dsx = X;
                dsy = Y;
                dsmx = MouseManager.X;
                dsmy = MouseManager.Y;
                d = true;
            }

            if (GetMouseCorner() > -1)
            {

            }

            if (d)
            {
                X = dsx + (MouseManager.X - dsmx);
                Y = dsy + (MouseManager.Y - dsmy);
            }

            if (MouseManager.MouseState == MouseState.None)
            {
                d = false;
            }
        }

        public virtual void HandleKey(KeyEvent key) { }
    }
}
