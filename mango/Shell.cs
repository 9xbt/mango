namespace mango
{
    public static class Shell
    {
        public static void Run(string input, SVGAIITerminal Console)
        {
            Console.Font = Resources.Font;
            Commands.Console = Console;

            input = input.Trim();
            string[] args = input.Split(' ');

            switch (args[0].Trim().ToLower())
            {
                case "":
                    break;

                case "?":
                    Commands.Help();
                    break;

                case "help":
                    Commands.Help();
                    break;

                case "info":
                    Commands.Info();
                    break;

                case "clear":
                    Commands.Clear();
                    break;

                case "reboot":
                    Commands.Reboot();
                    break;

                case "shutdown":
                    Commands.Shutdown();
                    break;

                case "setfont":
                    Commands.SetFont(input, args);
                    break;

                case "loadkeys":
                    Commands.LoadKeys(args);
                    break;

                case "su":
                    Commands.SU(args);
                    break;

                case "who":
                    Commands.Who();
                    break;

                case "ls":
                    Commands.LS();
                    break;

                case "cd":
                    Commands.CD(input, args);
                    break;

                case "touch":
                    Commands.Touch(input, args);
                    break;

                case "mkdir":
                    Commands.MkDir(input, args);
                    break;

                case "cat":
                    Commands.Cat(input, args);
                    break;

                case "rm":
                    Commands.RM(input, args);
                    break;

                case "rmdir":
                    Commands.RmDir(input, args);
                    break;

                case "echo":
                    Commands.Echo(input.Split("echo ")[1].Split(" >> "));
                    break;

                default:
                    Console.WriteLine($"Command \"{args[0].Trim()}\" not found.", SVGAIIColor.Red);
                    break;
            }
        }
    }
}
