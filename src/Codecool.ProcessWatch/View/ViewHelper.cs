using System;

namespace Codecool.ProcessWatch.View
{
    public static class ViewHelper
    {
        public static void HelpInfo()
        {
            string help = @"    Main Menu commands:
        --exit                  exits from application (also works in submenus).
            
    Submenu commands:
        --gu                    goes up to the top menu,
        --exit                  exits from application,
        --rf                    refreshes the processes view,
        --help                  shows usage information,
        --kill=[PID]            kills the process with given PID
        --kill-visible          kills all visible processes (be careful with this
                                function). It could cause problem and force 
                                to turn off the computer.

    Submenu ""Filter processes by name"":
        --search=[phrase]       searches given phrase (to show all processes again
                                write --search=).";

            Console.WriteLine(help);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
