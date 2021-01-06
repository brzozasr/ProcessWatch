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
        --search=[phrase]       searches processes name with given phrase 
                                (to show all processes again write --search=).
    
    Submenu ""Filter processes by day"":
        --day=[DD]              searches for processes that have started on 
                                a given day takes into account all processes 
                                from each month.

    Submenu ""Filter processes by month"":
        --month=[MM]            searches for processes that have started on 
                                a given month takes into account all 
                                processes from each month.

    Submenu ""Filter processes by memory usage"":
        --memory=[0.0001]       searches for processes that have processes 
                                physical memory usage greater than given 
                                memory in MB. Allow range is from 0.0001 
                                to 9999999999.9999 MB.

    Submenu ""Filter processes by user / total CPU time"":
        --time=[0.001]          searches for processes that have processes 
                                user / total  CPU time greater than given 
                                time in seconds. Allow range is from 0.001 
                                to 9999999999.999 sec.";

            Console.WriteLine(help);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void AboutApp()
        {
            string about = @"        Process Watch
        
        Version: 1.0

        Authors:
        Sławomir Brzozowski, Michał Orłowski

        Web page:
        https://github.com/brzozasr/ProcessWatch

        License:
        Apache License
        Version 2.0, January 2004
        http://www.apache.org/licenses/";

            Console.WriteLine(about);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}