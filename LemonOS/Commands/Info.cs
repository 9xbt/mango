using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonOS
{
    public static partial class Commands
    {
        public static void Info()
        {
            Console.WriteLine($"{Kernel.Logo}", SVGAIIColor.Yellow);
            Console.WriteLine($"{Kernel.Version}{Kernel.Copyright}\nThis program comes with ABSOLUTELY NO WARRANTY.\nThis is free software, and you are welcome to redistribute it under certain conditions.\nFor more info, see https://www.gnu.org/licenses.\n");
        }
    }
}
