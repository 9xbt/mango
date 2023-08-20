using System;
using System.IO;
using System.Collections.Generic;
using Cosmos.System;
using Cosmos.System.ScanMaps;

namespace mango
{
    public static partial class Commands
    {
        private static readonly Dictionary<string, ScanMapBase> scanMaps = new()
        {
            { "gb-uk", new GBStandardLayout() },
            { "us-us", new USStandardLayout() },
            { "de-de", new DEStandardLayout() },
            { "es-es", new ESStandardLayout() },
            { "fr-fr", new FRStandardLayout() },
            { "tr-tr", new TRStandardLayout() }
        };

        public static void LoadKeys(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (args.Length > 2)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            string keymapTo = args[1].Trim().Replace("/", "-");

            if (keymapTo == "-l")
            {
                Console.WriteLine("loadkeys: Available keymaps: gb-uk, us-us, de-de, es-es, fr-fr, tr-tr", SVGAIIColor.Gray);
            }
            else if (scanMaps.TryGetValue(keymapTo, out ScanMapBase map))
            {
                try
                {
                    KeyboardManager.SetKeyLayout(map);
                    File.WriteAllText(@"0:\LunarOS\keymap.txt", keymapTo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"loadkeys: Failed to set keyboard layout: {ex.Message}", SVGAIIColor.Red);
                    return;
                }
            }
            else
            {
                Console.WriteLine($"loadkeys: Unknown keyboard layout, type \"loadkeys -l\" to list layouts", SVGAIIColor.Gray);
            }
        }
    }
}
