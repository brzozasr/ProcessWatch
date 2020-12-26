using System;
using System.Collections.Generic;
using System.Text;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch
{
    public static class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Write number:");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    Console.Clear();
                    Menu(number);
                }
                
                Console.WriteLine("Write process ID:");
                string inputId = Console.ReadLine();
                if (Int32.TryParse(inputId, out var processId))
                {
                    Console.Clear();
                    ProcessWatchApplication.KillProcess(processId);
                }
            }
        }

        private static void Menu(int pageNo)
        {
            int pageSize = 20;

            var pagination = ProcessWatchApplication.SelectProcessesByName(pageSize, pageNo, "chro");

            StringBuilder sb = new StringBuilder();

            string line = new String('-', 193);

            sb.Append($"+{line}+\n");
            foreach (var memoryItem in pagination.ProcessesList)
            {
                sb.Append($"| {Converters.IntNullConverter(memoryItem.ProcessId),7} " +
                          $"| {Converters.StrNullConverter(memoryItem.ProcessName),-16} " +
                          $"| {Converters.ByteConverter(memoryItem.PhysicalMemoryUsage),10}  " +
                          $"| {Converters.StrNullConverter(memoryItem.PriorityClass),-11} " +
                          $"| {Converters.TimeConverter(memoryItem.UserProcessorTime),10} " +
                          $"| {Converters.TimeConverter(memoryItem.PrivilegedProcessorTime),10} " +
                          $"| {Converters.TimeConverter(memoryItem.TotalProcessorTime),10} " +
                          $"| {Converters.IntNullConverter(memoryItem.ThreadsNumber),3} " +
                          $"| {Converters.DateTimeNullConverter(memoryItem.StartTime),19} " +
                          $"| {Converters.IntNullConverter(memoryItem.BasePriority),2} " +
                          $"| {Converters.ByteConverter(memoryItem.PagedSystemMemorySize),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PagedMemorySize),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PeakPhysicalMemoryUsage),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PeakPagedMemorySize),10} " +
                          $"| {Converters.StrNullConverter(memoryItem.StartInfoUserName),-10} |\n");
            }

            sb.Append($"+{line}+\n");
            Console.WriteLine(sb.ToString());

            Console.WriteLine($"Number of pages: {pagination.NumberOfPages}");
        }
    }
}