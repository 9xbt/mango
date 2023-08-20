namespace mango
{
    public static partial class Commands
    {
        public static void Info()
        {
            Console.WriteLine();
            Console.DrawImage(Resources.Logo, false);
            Console.WriteLine($"\nThe mango Operating System\n{Kernel.Version}{Kernel.Copyright}\nMade by xrc2\n\nThis program comes with ABSOLUTELY NO WARRANTY.\nThis is free software, and you are welcome to redistribute it under certain conditions.\nFor more info, see https://www.gnu.org/licenses.\n", SVGAIIColor.Gray);
        }
    }
}
