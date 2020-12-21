using System;
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
            // MemoryItemProcess prroc = new MemoryItemProcess();
            // Console.WriteLine(prroc.GetMemoryItemProcessById(73066).ProcessName);
            //
            // MemoryItemProcess list = new MemoryItemProcess();
            //
            // Console.WriteLine(list.GetProcessesList().Count);
            // Console.WriteLine(list.GetProcessesList()[0].ProcessName);
            
            
            // foreach (MemoryItemProcess itemProcess in list.GetMemoryItemProcessList())
            // {
            //     Console.WriteLine(itemProcess.ProcessId + " " + itemProcess.ProcessName);
            // }

            foreach (var pr in Process.GetProcesses())
            {
                if (pr.HasExited)
                {
                    Console.WriteLine(pr.PriorityClass.ToString());
                }
            }
        }
    }
}
