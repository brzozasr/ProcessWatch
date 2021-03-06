﻿using System;
using System.Text;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.GUI;
using Codecool.ProcessWatch.View;


namespace Codecool.ProcessWatch
{
    public static class Program
    {
        public static bool IsMainLoopRun = true;
        private const int PageSize = 25;
        private static int _pageNo = 1;

        public static void Main()
        {
            ScreenView screenView = new ScreenView();
            Console.Clear();

            while (IsMainLoopRun)
            {
                Console.Write(screenView.MainMenu().ToString());
                Console.Write("Enter the number or write \"--exit\" to finish: ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= 13)
                    {
                        Console.Clear();
                        switch (number)
                        {
                            case 1:
                                screenView.GetAllProcesses(PageSize, _pageNo);
                                break;
                            case 2:
                                screenView.GetProcessesByName(PageSize, _pageNo, "");
                                break;
                            case 3:
                                var todayDate = DateTime.Today;
                                screenView.GetProcessesStartedAtDate(PageSize, _pageNo, todayDate.Day, todayDate.Month,
                                    todayDate.Year);
                                break;
                            case 4:
                                var todayDay = DateTime.Today.Day;
                                screenView.GetProcessesStartedAtDay(PageSize, _pageNo, todayDay);
                                break;
                            case 5:
                                var todayMonth = DateTime.Today.Month;
                                screenView.GetProcessesStartedAtMonth(PageSize, _pageNo, todayMonth);
                                break;
                            case 6:
                                var todayBeforeDate = DateTime.Today;
                                screenView.GetProcessesStartedBeforeDate(PageSize, _pageNo, todayBeforeDate.Day,
                                    todayBeforeDate.Month, todayBeforeDate.Year);
                                break;
                            case 7:
                                var todayAfterDate = DateTime.Today;
                                screenView.GetProcessesStartedAfterDate(PageSize, _pageNo, todayAfterDate.Day,
                                    todayAfterDate.Month, todayAfterDate.Year);
                                break;
                            case 8:
                                screenView.GetProcessesMemoryUsageGreaterThan(PageSize, _pageNo, 1);
                                break;
                            case 9:
                                screenView.GetProcessesUserCpuTimeGreaterThan(PageSize, _pageNo, 1);
                                break;
                            case 10:
                                screenView.GetProcessesTotalCpuTimeGreaterThan(PageSize, _pageNo, 1);
                                break;
                            case 11:
                                _ = new StartGui();
                                break;
                            case 12:
                                ViewHelper.HelpInfo();
                                break;
                            case 13:
                                ViewHelper.AboutApp();
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else if (input == "--exit")
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    continue;
                }
            }
        }

        private static void GetMenu(int pageNo)
        {
            int pageSize = 25;

            var pagination = ProcessWatchApplication.AllProcesses(pageSize, pageNo);

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