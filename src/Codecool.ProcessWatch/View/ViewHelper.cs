using System;

namespace Codecool.ProcessWatch.View
{
    public static class ViewHelper
    {
        public static void HelpInfo()
        {
            string help = @"    Main Menu commands:
        --exit              exits from application,
        --help              shows usage information (works also in submenu),
        --about             shows about application information.
            
    Submenu commands:
        --gu                goes up to the top menu,
        --rf                refreshes the processes view,
        --kill=[PID]        kills the process with given PID.";

            Console.WriteLine(help);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
