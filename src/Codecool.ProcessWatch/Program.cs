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
            DataHelper dataHelper = new DataHelper();
            foreach (var memoryItem in dataHelper.GetMemoryItemProcessList())
            {
                Console.WriteLine($"{memoryItem.ProcessId} => {memoryItem.ProcessName} => {memoryItem.ThreadsNumber}");
            }

            foreach (var process in Process.GetProcesses())
            {
                Console.WriteLine(process.MainWindowHandle);
            }
        }
    }
}