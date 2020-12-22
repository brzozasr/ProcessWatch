using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.GUI;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch
{
    public static class Program
    {
        public static void Main()
        {
            // foreach (var p in Process.GetProcesses())
            // {
            //     try
            //     {
            //         Console.WriteLine(p.StartInfo);
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine(e.Message);
            //     }
            // }


            MemoryItemProcess processes = new MemoryItemProcess();

            int i = 0;
            foreach (var process in processes.GetMemoryItemProcessList())
            {
                Console.WriteLine(process.ProcessId + " " + process.PriorityClass + " " + process.UserProcessorTime);
                i++;
            }

            Console.WriteLine("Processes: " + i);
        }
    }
}