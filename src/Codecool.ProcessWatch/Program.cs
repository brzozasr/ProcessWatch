using System;
using System.Text;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch
{
    public static class Program
    {
        public static void Main()
        {
            StringBuilder sb = new StringBuilder();

            DataHelper dataHelper = new DataHelper();

            string line = new String('-', 167);

            sb.Append($"+{line}+\n");
            foreach (var memoryItem in dataHelper.GetMemoryItemProcessList())
            {
                sb.Append($"| {Converters.IntNullConverter(memoryItem.ProcessId),7} " +
                          $"| {Converters.StrNullConverter(memoryItem.ProcessName),-16} " +
                          $"| {Converters.ByteConverter(memoryItem.PhysicalMemoryUsage),10}  " +
                          $"| {Converters.StrNullConverter(memoryItem.PriorityClass),-11} " +
                          $"| {Converters.TimeConverter(memoryItem.UserProcessorTime),10} " +
                          $"| {Converters.TimeConverter(memoryItem.PrivilegedProcessorTime),10} " +
                          $"| {Converters.TimeConverter(memoryItem.TotalProcessorTime),10} " +
                          $"| {Converters.IntNullConverter(memoryItem.ThreadsNumber),3} " +
                          $"| {Converters.IntNullConverter(memoryItem.BasePriority),2} " +
                          $"| {Converters.ByteConverter(memoryItem.PagedSystemMemorySize),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PagedMemorySize),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PeakPhysicalMemoryUsage),10} " +
                          $"| {Converters.ByteConverter(memoryItem.PeakPagedMemorySize),5} " +
                          $"| {Converters.StrNullConverter(memoryItem.StartInfoUserName),-10} |\n");
            }

            sb.Append($"+{line}+\n");
            Console.WriteLine(sb.ToString());
        }
    }
}