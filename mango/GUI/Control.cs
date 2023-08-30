using Cosmos.System;
using PrismAPI.Graphics;

namespace mango.GUI
{
    public abstract class Control
    {
        public Window Parent;
        public int X, Y;
        public ushort Width, Height;

        public Canvas Contents;

        public bool IsMouseOver
        {
            get
            {
                return MouseManager.X > Parent.X + X && MouseManager.X < Parent.X + X + Width && MouseManager.Y > Parent.Y + Y && MouseManager.Y < Parent.Y + Y + Height;
            }
        }

        public Control(Window Parent, int X, int Y, ushort Width, ushort Height)
        {
            this.Parent = Parent;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;

            Contents = new Canvas(Width, Height);

            Render();
        }

        public abstract void Render();

        public virtual void Update() { }
    }
}
