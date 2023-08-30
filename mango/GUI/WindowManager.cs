//#define SHOW_FPS

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
        private static bool needToAddTerminal = false;

        public static Display Screen = Kernel.Screen;
        public static List<Window> Windows = new List<Window>(10);

        public static Window FocusedWindow
        {
            get
            {
                if (Windows.Count < 1)
                    return null;

                return Windows[^1];
            }
        }

        public static Window LastFocusedWindow = null;

        public static void AddWindow(Window wnd) => Windows.Add(wnd);

        public static void RemoveWindow(Window wnd) { try { if (Windows.Contains(wnd)) Windows.Remove(wnd); } catch { } }

        public static void MoveWindowToFront(Window wnd)
        {
            if (!wnd.Name.StartsWith("WM.") && Windows[^1] != wnd)
            {
                RemoveWindow(wnd);
                AddWindow(wnd);
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

        public static void Start()
        {
            var term = new Terminal();
            term.Console.WriteLine($"Welcome back, {Kernel.Username}.\n");
            term.Console.DrawImage(Resources.Logo, false);
            term.Console.WriteLine($"\nThe mango Operating System\n{Kernel.Version}{Kernel.Copyright}");
            term.DrawPrompt();

            term.startX = term.Console.CursorX;
            term.startY = term.Console.CursorY;

            AddWindow(new Desktop());
            AddWindow(term);

            Logger.SuccessLog("Desktop enviorment started.");
        }

        public static void Update(Window except = null)
        {
            Handle(except);
            Render();
        }

        public static void Handle(Window except = null)
        {
            foreach (Window wnd in Windows)
            {
                if (wnd != null)
                {
                    try
                    {
                        if (except != null)
                        {
                            if (except != wnd)
                            {
                                wnd.Update();
                            }
                        }
                        else
                        {
                            wnd.Update();
                        }
                    }
                    catch (Exception ex)
                    {
                        AddWindow(new Dialogue($"The application {wnd?.Name} has been terminated\n{ex}", DialogueIcon.Error));
                        Render();
                    }
                }
            }

            if (KeyboardManager.TryReadKey(out var key))
            {
                if (KeyboardManager.AltPressed && key.Key == ConsoleKeyEx.T)
                {
                    needToAddTerminal = true;
                }
                else if (KeyboardManager.AltPressed && key.Key == ConsoleKeyEx.F4 && !FocusedWindow.Name.StartsWith("WM."))
                {
                    RemoveWindow(FocusedWindow);
                }
                else
                {
                    FocusedWindow?.HandleKey(key);
                }
            }

            if (needToAddTerminal)
            {
                var term = new Terminal();
                term.DrawPrompt();

                term.startX = term.Console.CursorX;
                term.startY = term.Console.CursorY;

                AddWindow(term);

                needToAddTerminal = false;
            }
        }

        public static void Render()
        {
            foreach (Window wnd in Windows)
            {
                if (wnd != null)
                {
                    Screen.DrawImage(wnd.X, wnd.Y, wnd.Contents, false);
                }
            }

            #if SHOW_FPS
            Screen.DrawString(2, 22, $"{Screen.GetFPS()} FPS", Resources.Font, PrismAPI.Graphics.Color.Black);
            #endif

            if (needToAddTerminal)
            {
                MouseDriver.Mouse = Resources.Busy;
            }
            MouseDriver.Update();

            Screen.Update();

            framesToHeapCollect--;
            if (framesToHeapCollect <= 0)
            {
                Heap.Collect();
                Resources.GenerateFont();
                framesToHeapCollect = 10;
            }

            LastFocusedWindow = FocusedWindow;
            MouseDriver.Mouse = Resources.Mouse;
            MouseDriver.MouseOffsetX = 0;
            MouseDriver.MouseOffsetY = 0;
            MouseDriver.LastMouseX = (int)MouseManager.X;
            MouseDriver.LastMouseY = (int)MouseManager.Y;
        }
    }
}
