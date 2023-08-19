using System;
using Cosmos.System;
using PrismAPI.Graphics;
using PrismAPI.Graphics.Fonts;

public class SVGAIITerminal
{
    #region Fields

    public int Width, Height;
    public int CursorX = 0, CursorY = 0;
    public Color ForegroundColor = Color.White;
    public Color BackgroundColor = Color.Black;
    public bool CursorVisible = true;

    public Canvas Contents;
    public Font Font;

    public Action Update;

    #endregion

    #region Constructors

    public SVGAIITerminal(int Width, int Height, Font Font, Action Update)
    {
        this.Width = Width / (Font.Size / 2);
        this.Height = Height / Font.Size;
        this.Font = Font;
        this.Update = Update;
        Contents = new Canvas((ushort)Width, (ushort)Height);
    }

    #endregion

    #region Functions

    public void Clear()
    {
        Contents.Clear();
        CursorX = 0;
        CursorY = 0;
    }

    public void Write(object str) => Write(str, ForegroundColor);

    public void Write(object str, Color color)
    {
        foreach (char c in str.ToString())
        {
            TryScroll();

            switch (c)
            {
                case '\n':
                    CursorX = 0;
                    CursorY++;
                    break;

                default:
                    Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                    Contents.DrawString(Font.Size / 2 * CursorX, Font.Size * CursorY, c.ToString(), Font, color);
                    CursorX++;
                    break;
            }
        }

        Update?.Invoke();
    }

    public void WriteLine(object str = null) => Write(str + "\n");

    public void WriteLine(object str, Color color) => Write(str + "\n", color);

    public ConsoleKeyInfo ReadKey(bool intercept = true)
    {
        while (true)
        {
            TryDrawCursor();

            if (KeyboardManager.TryReadKey(out var key))
            {
                if (intercept == false)
                {
                    Write(key.KeyChar);
                }

                bool xShift = (key.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift;
                bool xAlt = (key.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt;
                bool xControl = (key.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control;

                return new ConsoleKeyInfo(key.KeyChar, key.Key.ToConsoleKey(), xShift, xAlt, xControl);
            }
        }
    }

    public string LastInput;

    public string ReadLine()
    {
        ForceDrawCursor();

        int startX = CursorX, startY = CursorY;
        string returnValue = string.Empty;

        bool reading = true;
        while (reading)
        {
            TryDrawCursor();

            if (KeyboardManager.TryReadKey(out var key))
            {
                switch (key.Key)
                {
                    case ConsoleKeyEx.Enter:
                        Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                        CursorX = 0;
                        CursorY++;
                        TryScroll();
                        LastInput = returnValue;
                        reading = false;
                        break;

                    case ConsoleKeyEx.Backspace:
                        if (!(CursorX == startX && CursorY == startY))
                        {
                            if (CursorX == 0)
                            {
                                Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                                CursorY--;
                                CursorX = Contents.Width / (Font.Size / 2) - 1;
                                Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                            }
                            else
                            {
                                Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                                CursorX--;
                                Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, BackgroundColor);
                            }

                            returnValue = returnValue.Remove(returnValue.Length - 1); // Remove the last character of the string
                        }

                        ForceDrawCursor();
                        break;

                    case ConsoleKeyEx.Tab:
                        Write('\t');
                        returnValue += new string(' ', 4);

                        ForceDrawCursor();
                        break;

                    case ConsoleKeyEx.UpArrow:
                        SetCursorPosition(startX, startY);
                        Write(new string(' ', returnValue.Length));
                        SetCursorPosition(startX, startY);
                        Write(LastInput);
                        returnValue = LastInput;

                        ForceDrawCursor();
                        break;

                    default:
                        if (KeyboardManager.ControlPressed)
                        {
                            if (key.Key == ConsoleKeyEx.L)
                            {
                                Clear();
                                returnValue = string.Empty;
                                reading = false;
                            }
                        }
                        else
                        {
                            Write(key.KeyChar.ToString());
                            TryScroll();
                            returnValue += key.KeyChar;
                        }

                        ForceDrawCursor();
                        break;
                }
            }
        }

        return returnValue;
    }

    public void SetCursorPosition(int x, int y)
    {
        CursorX = x;
        CursorY = y;
    }

    public (int Left, int Top) GetCursorPosition()
    {
        return (CursorX, CursorY);
    }

    public void Beep(uint freq = 800, uint duration = 125)
    {
        PCSpeaker.Beep(freq, duration);
    }

    #endregion

    #region Private fields

    private byte lastSecond = Cosmos.HAL.RTC.Second;
    private bool cursorState = true;

    #endregion

    #region Private functions

    private void TryScroll()
    {
        if (CursorX >= Width)
        {
            CursorX = 0;
            CursorY++;
        }

        while (CursorY >= Height)
        {
            Contents.DrawImage(0, -Font.Size, Contents, false);
            Contents.DrawFilledRectangle(0, Contents.Height - Font.Size, Contents.Width, Font.Size, 0, BackgroundColor);
            Update?.Invoke();
            CursorY--;
        }
    }

    private void ForceDrawCursor()
    {
        Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, ForegroundColor);
        Update?.Invoke();
    }

    private void TryDrawCursor()
    {
        if (Cosmos.HAL.RTC.Second != lastSecond)
        {
            Contents.DrawFilledRectangle(Font.Size / 2 * CursorX, Font.Size * CursorY, Convert.ToUInt16(Font.Size / 2), Font.Size, 0, cursorState ? ForegroundColor : BackgroundColor);
            Update?.Invoke();

            lastSecond = Cosmos.HAL.RTC.Second;
            cursorState = !cursorState;
        }
    }

    #endregion
}
