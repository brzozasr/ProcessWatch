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
            
            string line = new String('-', 139);

            Console.WriteLine($"+{line}+");
            foreach (var memoryItem in dataHelper.GetMemoryItemProcessList())
            {
                Console.WriteLine($"|{memoryItem.ProcessId, 7} | {memoryItem.ProcessName, -16} " +
                                  $"| {memoryItem.PhysicalMemoryUsage, 7} | {memoryItem.BasePriority, 2} " +
                                  $"| {memoryItem.PriorityClass, -11} | {memoryItem.UserProcessorTime, 10} " +
                                  $"| {memoryItem.PrivilegedProcessorTime, 10} | {memoryItem.TotalProcessorTime, 10} " +
                                  $"| {memoryItem.PagedSystemMemorySize, 5} | {memoryItem.PagedMemorySize, 5} " +
                                  $"| {memoryItem.PeakPhysicalMemoryUsage, 5} | {memoryItem.PeakPagedMemorySize, 5} " +
                                  $"| {memoryItem.StartInfoUserName, 4} | {memoryItem.ThreadsNumber, 3}|");
            }
            
            Console.WriteLine($"+{line}+");
            
        }
    }
}