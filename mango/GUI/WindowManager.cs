using System;
using System.Collections.Generic;
using Cosmos.Core.Memory;
using Cosmos.System;
using mango.GUI.Apps;
using PrismAPI.Hardware.GPU;

namespace mango.GUI
{
    public static class WindowManager
    {
        private static short framesToHeapCollect = 10;

        public static Display Screen = Kernel.Screen;
        public static List<Window> Windows = new List<Window>(10);
        public static Action<Window> DesktopWindowHook;

        public static Window FocusedWindow
        {
            get
            {
                if (Windows.Count < 1)
                {
                    return null;
                }

                return Windows[^1];
            }
        }

        public static void AddWindow(Window wnd)
        {
            if (!Windows.Contains(wnd))
            {
                int amountOfThese = GetAmountOfWindowsByName(wnd.Name);

                if (amountOfThese > 0)
                {
                    wnd.Name += $" <{amountOfThese}>";
                }

                Windows.Add(wnd);

                DesktopWindowHook?.Invoke(wnd);
            }
        }

        public static void MoveWindowToFront(Window wnd)
        {
            if (!wnd.Name.StartsWith("WM.") && Windows[^1] != wnd)
            {
                Windows.Remove(wnd);
                Windows.Add(wnd);
                DesktopWindowHook?.Invoke(wnd);
            }
        }

        public static int GetAmountOfWindowsByName(string wnd)
        {
            int counter = 0;

            for (int i = 0; i < Windows.Count; i++)
            {
                if (Windows[i].Name == wnd)
                    counter++;
            }

            return counter;
        }

        public static void Update()
        {
            for (int i = 0; i < Windows.Count; i++)
            {
                if (Windows[i] != null)
                {
                    Windows[i].Update();

                    Screen.DrawImage(Windows[i].X, Windows[i].Y, Windows[i].Contents, false);
                }
            }

            if (KeyboardManager.TryReadKey(out var key))
            {
                if (KeyboardManager.AltPressed)
                {
                    switch (key.Key)
                    {
                        case ConsoleKeyEx.T:
                            var term = new Terminal();
                            term.DrawPrompt();

                            AddWindow(term);
                            break;

                        case ConsoleKeyEx.F4:
                            Windows.Remove(FocusedWindow);
                            break;
                    }
                }
                else
                {
                    Windows[^1].HandleKey(key);
                }
            }

            MouseDriver.Update();

            Screen.DrawString(2, 22, $"{Screen.GetFPS()} FPS", Resources.Font, PrismAPI.Graphics.Color.Black);

            Screen.Update();

            framesToHeapCollect--;

            if (framesToHeapCollect <= 0)
            {
                Heap.Collect();
                framesToHeapCollect = 10;
            }
        }
    }
}
