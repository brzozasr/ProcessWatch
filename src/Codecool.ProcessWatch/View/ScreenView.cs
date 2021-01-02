using System;
using System.Collections.Generic;
using System.Text;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch.View
{
    public class ScreenView
    {
        public StringBuilder MainMenu()
        {
            StringBuilder sb = new StringBuilder();

            string[] menuItems = new[]
            {
                "MAIN MENU:",
                " (1)  All processes",
                " (2)  Filter processes by name",
                " (3)  Filter processes started at given date",
                " (4)  Filter processes started at given day",
                " (5)  Filter processes started at given month",
                " (6)  Filter processes started before given date",
                " (7)  Filter processes started after given date",
                " (8)  Filter processes physical memory usage greater than",
                " (9)  Filter processes user CPU time greater than",
                "(10)  Filter processes total CPU time greater than",
                "(11)  Run Process Watch GUI"
            };

            foreach (var menuItem in menuItems)
            {
                sb.Append($"{menuItem}\n");
            }

            return sb;
        }

        public void GetAllProcesses(int pageSize, int pageNo)
        {
            while (true)
            {
                var allProcesses = ProcessWatchApplication.AllProcesses(pageSize, pageNo);
                PrintProcessesView(allProcesses.ProcessesList);
                Console.WriteLine($"Page {pageNo} of {allProcesses.NumberOfPages}");
                Console.Write($"Enter the page number (1 - {allProcesses.NumberOfPages}) to go next page or write \"go up\" to go main menu: ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= allProcesses.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else if (input == "go up")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    continue;
                }
            }
        }
        
        public void GetProcessesByName(int pageSize, int pageNo, string searchString)
        {
            while (true)
            {
                var allProcesses = ProcessWatchApplication.SelectProcessesByName(pageSize, pageNo, searchString);
                PrintProcessesView(allProcesses.ProcessesList);
                Console.WriteLine($"Page {pageNo} of {allProcesses.NumberOfPages}");
                Console.Write($"Enter the page number (1 - {allProcesses.NumberOfPages}) to go next page or write \"go up\" to go main menu: ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= allProcesses.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else if (input == "go up")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    searchString = input;
                    pageNo = 1;
                }
            }
        }

        public void PrintProcessesView(List<MemoryItemProcess> processesList)
        {
            StringBuilder sb = new StringBuilder();

            string line = new String('-', 145);

            Console.ForegroundColor = ConsoleColor.Green;
            
            sb.Append($"+{line}+\n");

            sb.Append($"| {"",7} " +
                      $"| {"",-16} " +
                      $"| {"Memory",12} " +
                      $"| {"",-11} " +
                      $"| {"User",12} " +
                      $"| {"Privilege",12} " +
                      $"| {"Total",12} " +
                      $"| {"",7} " +
                      $"| {"",19} " +
                      $"| {"Base",8} |\n");

            sb.Append($"| {"PID",7} " +
                      $"| {"Process Name",-16} " +
                      $"| {"Usage",12} " +
                      $"| {"Priority",-11} " +
                      $"| {"CPU Time",12} " +
                      $"| {"CPU Time",12} " +
                      $"| {"CPU Time",12} " +
                      $"| {"Threads",7} " +
                      $"| {"Start Time",19} " +
                      $"| {"Priority",8} |\n");

            sb.Append($"+{line}+\n");
            foreach (var memoryItem in processesList)
            {
                sb.Append($"| {Converters.IntNullConverter(memoryItem.ProcessId),7} " +
                          $"| {Converters.StrNullConverter(memoryItem.ProcessName),-16} " +
                          $"| {Converters.ByteConverter(memoryItem.PhysicalMemoryUsage),11}  " +
                          $"| {Converters.StrNullConverter(memoryItem.PriorityClass),-11} " +
                          $"| {Converters.TimeConverter(memoryItem.UserProcessorTime),12} " +
                          $"| {Converters.TimeConverter(memoryItem.PrivilegedProcessorTime),12} " +
                          $"| {Converters.TimeConverter(memoryItem.TotalProcessorTime),12} " +
                          $"| {Converters.IntNullConverter(memoryItem.ThreadsNumber),7} " +
                          $"| {Converters.DateTimeNullConverter(memoryItem.StartTime),19} " +
                          $"| {Converters.IntNullConverter(memoryItem.BasePriority),8} |\n"
                    // $"| {Converters.ByteConverter(memoryItem.PagedSystemMemorySize),10} " +
                    // $"| {Converters.ByteConverter(memoryItem.PagedMemorySize),10} " +
                    // $"| {Converters.ByteConverter(memoryItem.PeakPhysicalMemoryUsage),10} " +
                    // $"| {Converters.ByteConverter(memoryItem.PeakPagedMemorySize),10} " +
                    // $"| {Converters.StrNullConverter(memoryItem.StartInfoUserName),-10} |\n"
                );
            }

            sb.Append($"+{line}+\n");

            Console.Write(sb.ToString());
            Console.ResetColor();
        }

        /* EXAMPLE OF PRINTING ALL PROCESSES WITH PAGINATION
        int pageSize = 20;

        var pagination = ProcessWatchApplication.AllProcesses(pageSize, 1);
            
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
                      $"| {Converters.DateTimeNullConverter(memoryItem.StartTime), 19} " +
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
        */


        /* EXAMPLE OF PRINTING PROCESSES SEARCHED BY NAME
         * (in this case searching pattern is "we") WITH PAGINATION
         
        int pageSize = 20;

        var pagination = ProcessWatchApplication.SelectProcessesByName(pageSize, 1, "we");

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
        */


        /* EXAMPLE OF PRINTING PROCESSES FILTERED BY DATE
         * (ALL PROCESSES BEFORE CHOSEN DATE)
         
        int pageSize = 20;

        var pagination = ProcessWatchApplication.SelectProcessesStartBeforeDate(pageSize, 1, day: 24);

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
        */


        /* EXAMPLE OF PRINTING PROCESSES FILTERED BY DAY OF START
         
        int pageSize = 20;

        var pagination = ProcessWatchApplication.SelectProcessesStartAtMonth(pageSize, 1, month: 12);

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
        */


        /* EXAMPLE OF PRINTING PROCESSES AT GIVEN DATE
         
        int pageSize = 20;

        var pagination = ProcessWatchApplication.SelectProcessesStartAtDate(pageSize, 1, 24, 12, 2020);

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
        */
    }
}