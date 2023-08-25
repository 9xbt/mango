namespace mango
{
    public static partial class Commands
    {
        public static void Help()
        {
            Console.WriteLine("help: All commands: ?, help, info, clear, reboot, shutdown, setfont, loadkeys, su, who, ls, cd, touch, mkdir, cat, rm, rmdir", SVGAIIColor.Gray);
        }
    }
}
