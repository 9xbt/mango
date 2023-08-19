namespace LemonOS
{
    public static class Logger
    {
        private static SVGAIITerminal Console = Kernel.Console;

        public static void InfoLog(string str)
        {
            Console.Write("[ INFO ] ", SVGAIIColor.Blue);
            Console.WriteLine(str);
        }

        public static void SuccessLog(string str)
        {
            Console.Write("[  OK  ] ", SVGAIIColor.Green);
            Console.WriteLine(str);
        }

        public static void WarnLog(string str)
        {
            Console.Write("[ WARN ] ", SVGAIIColor.Yellow);
            Console.WriteLine(str);
        }

        public static void ErrorLog(string str)
        {
            Console.Write("[ FAIL ] ", SVGAIIColor.Red);
            Console.WriteLine(str);

            while (true);
        }
    }
}
