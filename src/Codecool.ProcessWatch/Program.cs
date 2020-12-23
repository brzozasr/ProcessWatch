using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
            
            string line = new String('-', 167);

            Console.WriteLine($"+{line}+");
            foreach (var memoryItem in dataHelper.GetMemoryItemProcessList())
            {
                Console.WriteLine($"| {Utilities.IntNullConverter(memoryItem.ProcessId), 7} " +
                                  $"| {Utilities.StrNullConverter(memoryItem.ProcessName), -16} " +
                                  $"| {Utilities.ByteConverter(memoryItem.PhysicalMemoryUsage), 10}  " +
                                  $"| {Utilities.StrNullConverter(memoryItem.PriorityClass), -11} " +
                                  $"| {Utilities.TimeConverter(memoryItem.UserProcessorTime), 10} " +
                                  $"| {Utilities.TimeConverter(memoryItem.PrivilegedProcessorTime), 10} " +
                                  $"| {Utilities.TimeConverter(memoryItem.TotalProcessorTime), 10} " +
                                  $"| {Utilities.IntNullConverter(memoryItem.ThreadsNumber), 3} " +
                                  $"| {Utilities.IntNullConverter(memoryItem.BasePriority), 2} " +
                                  $"| {Utilities.ByteConverter(memoryItem.PagedSystemMemorySize), 10} " +
                                  $"| {Utilities.ByteConverter(memoryItem.PagedMemorySize), 10} " +
                                  $"| {Utilities.ByteConverter(memoryItem.PeakPhysicalMemoryUsage), 10} " +
                                  $"| {Utilities.ByteConverter(memoryItem.PeakPagedMemorySize), 5} " +
                                  $"| {Utilities.StrNullConverter(memoryItem.StartInfoUserName), -10} |");
            }
            
            Console.WriteLine($"+{line}+");
            
        }
    }
}