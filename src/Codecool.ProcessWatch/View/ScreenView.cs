using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;

namespace Codecool.ProcessWatch.View
{
    public class ScreenView
    {
        private StringBuilder _messenger = new StringBuilder();
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
                "(11)  Run Process Watch GUI",
                "(12)  Help information",
                "(13)  About application"
            };

            foreach (var menuItem in menuItems)
            {
                sb.Append($"{menuItem}\n");
            }

            return sb;
        }

        public void GetAllProcesses(int pageSize, int pageNo)
        {
            string patternKill = @"^--kill=[0-9]+$";
            Regex regxKill = new Regex(patternKill);
            
            while (true)
            {
                var allProcesses = ProcessWatchApplication.AllProcesses(pageSize, pageNo);
                PrintProcessesView(allProcesses.ProcessesList);
                
                Console.Write("ALL PROCESSES: ");
                int startPage = 1;
                if (allProcesses.NumberOfPages == 0)
                {
                    Console.WriteLine($"Page 0 of {allProcesses.NumberOfPages}");
                    startPage = 0;
                }
                else
                {
                    Console.WriteLine($"Page {pageNo} of {allProcesses.NumberOfPages}");
                    startPage = 1;
                }
                
                Console.WriteLine("To go to the top menu write \"--gu\".");

                if (!string.IsNullOrEmpty(_messenger.ToString()))
                {
                    char[] charsToTrim = {' ', '\n', '\t'};
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(_messenger.ToString().Trim(charsToTrim));
                    _messenger.Clear();
                    Console.ResetColor();
                }
                
                Console.Write($"Enter the page number ({startPage} - {allProcesses.NumberOfPages}) to go next page: ");
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
                        _messenger.Append($"You have entered the wrong page number ({number}).");
                        continue;
                    }
                }
                else if (input == "--rf")
                {
                    ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    Console.Clear();
                    continue;
                }
                else if (input == "--gu")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "--help")
                {
                    Console.Clear();
                    ViewHelper.HelpInfo();
                    continue;
                }
                else if (input == "--exit")
                {
                    Program.IsMainLoopRun = false;
                    break;
                }
                else if (!string.IsNullOrEmpty(input) && regxKill.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string prosessIdStr = input.Substring(position);

                    if (Int32.TryParse(prosessIdStr , out var prosessId))
                    {
                        Console.Write("Are you sure you want to kill the process (y/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation == "y")
                        {
                            Console.Clear();
                            string message = ProcessWatchApplication.KillProcess(prosessId);
                            _messenger.Append($"{message}\n");
                            pageNo = 1;
                            ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"Process with an Id of {prosessIdStr} is not running.");
                    }
                }
                else if (input == "--kill-visible")
                {
                    Console.Write("Are you sure you want to kill all visible processes (y/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        StringBuilder messages = ProcessWatchApplication.KillProcesses(ProcessWatchApplication.TmpList);
                        _messenger.Append(messages);
                        pageNo = 1;
                        ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    }
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    _messenger.Append($"You have entered incorrect data ({input}).");
                    continue;
                }
            }
        }

        public void GetProcessesByName(int pageSize, int pageNo, string searchString)
        {
            string patternSearch = @"^--search=.*$";
            Regex regxSearch = new Regex(patternSearch);
            
            string patternKill = @"^--kill=[0-9]+$";
            Regex regxKill = new Regex(patternKill);
            
            while (true)
            {
                var processesByName = ProcessWatchApplication.SelectProcessesByName(pageSize, pageNo, searchString);
                PrintProcessesView(processesByName.ProcessesList);
                
                Console.Write("PROCESSES FILTER BY NAME: ");
                int startPage = 1;
                if (processesByName.NumberOfPages == 0)
                {
                    Console.WriteLine($"Page 0 of {processesByName.NumberOfPages}");
                    startPage = 0;
                }
                else
                {
                    Console.WriteLine($"Page {pageNo} of {processesByName.NumberOfPages}");
                    startPage = 1;
                }
                
                Console.WriteLine("To go to the top menu write \"--gu\".");
                
                if (!string.IsNullOrEmpty(_messenger.ToString()))
                {
                    char[] charsToTrim = {' ', '\n', '\t'};
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(_messenger.ToString().Trim(charsToTrim));
                    _messenger.Clear();
                    Console.ResetColor();
                }
                
                Console.Write($"Enter the page number ({startPage} - {processesByName.NumberOfPages}) to go next page or write searching phrase (--search=phrase): ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= processesByName.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You have entered the wrong page number ({number}).");
                        continue;
                    }
                }
                else if (input == "--rf")
                {
                    ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    Console.Clear();
                    continue;
                }
                else if (input == "--gu")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "--help")
                {
                    Console.Clear();
                    ViewHelper.HelpInfo();
                    continue;
                }
                else if (input == "--exit")
                {
                    Program.IsMainLoopRun = false;
                    break;
                }
                else if (input != null && regxSearch.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string search = input.Substring(position);

                    Console.Clear();
                    searchString = search;
                    pageNo = 1;
                }
                else if (!string.IsNullOrEmpty(input) && regxKill.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string prosessIdStr = input.Substring(position);

                    if (Int32.TryParse(prosessIdStr , out var prosessId))
                    {
                        Console.Write("Are you sure you want to kill the process (y/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation == "y")
                        {
                            Console.Clear();
                            string message = ProcessWatchApplication.KillProcess(prosessId);
                            _messenger.Append($"{message}\n");
                            pageNo = 1;
                            ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"Process with an Id of {prosessIdStr} is not running.");
                    }
                }
                else if (input == "--kill-visible")
                {
                    Console.Write("Are you sure you want to kill all visible processes (y/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        StringBuilder messages = ProcessWatchApplication.KillProcesses(ProcessWatchApplication.TmpList);
                        _messenger.Append(messages);
                        pageNo = 1;
                        ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    }
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    _messenger.Append($"You have entered incorrect data ({input}).");
                    continue;
                }
            }
        }
        
        public void GetProcessesStartedAtDate(int pageSize, int pageNo, int day, int month, int year)
        {
            string patternDate = @"^([0-3])([0-9]).([0-1])([0-9]).([2-3])([0-9])([0-9])([0-9])$";
            Regex regxDate = new Regex(patternDate);
            
            string patternKill = @"^--kill=[0-9]+$";
            Regex regxKill = new Regex(patternKill);
            
            while (true)
            {
                var processesAtDate = ProcessWatchApplication.SelectProcessesStartAtDate(pageSize, pageNo, day, month, year);
                PrintProcessesView(processesAtDate.ProcessesList);
                
                Console.Write("PROCESSES FILTER BY DATE: ");
                int startPage = 1;
                if (processesAtDate.NumberOfPages == 0)
                {
                    Console.WriteLine($"Page 0 of {processesAtDate.NumberOfPages}");
                    startPage = 0;
                }
                else
                {
                    Console.WriteLine($"Page {pageNo} of {processesAtDate.NumberOfPages}");
                    startPage = 1;
                }

                Console.WriteLine("To go to the top menu write \"--gu\".");
                
                if (!string.IsNullOrEmpty(_messenger.ToString()))
                {
                    char[] charsToTrim = {' ', '\n', '\t'};
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(_messenger.ToString().Trim(charsToTrim));
                    _messenger.Clear();
                    Console.ResetColor();
                }
                
                Console.Write($"Enter the page number ({startPage} - {processesAtDate.NumberOfPages}) to go next page or write date (eg. DD.MM.YYYY): ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= processesAtDate.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You have entered the wrong  page number ({number}).");
                        continue;
                    }
                }
                else if (input == "--rf")
                {
                    ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    Console.Clear();
                    continue;
                }
                else if (input == "--gu")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "--help")
                {
                    Console.Clear();
                    ViewHelper.HelpInfo();
                    continue;
                }
                else if (input == "--exit")
                {
                    Program.IsMainLoopRun = false;
                    break;
                }
                else if (!string.IsNullOrEmpty(input) && regxDate.IsMatch(input))
                {
                    var arrayDate = input.Split('.');

                    try
                    {
                        _ = new DateTime(Int32.Parse(arrayDate[2]), Int32.Parse(arrayDate[1]), Int32.Parse(arrayDate[0]));
                        day = Int32.Parse(arrayDate[0]);
                        month = Int32.Parse(arrayDate[1]);
                        year = Int32.Parse(arrayDate[2]);
                        pageNo = 1;
                        Console.Clear();
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        _messenger.Append($"You entered an invalid date ({input})");
                        continue;
                    }
                }
                else if (!string.IsNullOrEmpty(input) && regxKill.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string prosessIdStr = input.Substring(position);

                    if (Int32.TryParse(prosessIdStr , out var prosessId))
                    {
                        Console.Write("Are you sure you want to kill the process (y/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation == "y")
                        {
                            Console.Clear();
                            string message = ProcessWatchApplication.KillProcess(prosessId);
                            _messenger.Append($"{message}\n");
                            pageNo = 1;
                            ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"Process with an Id of {prosessIdStr} is not running.");
                    }
                }
                else if (input == "--kill-visible")
                {
                    Console.Write("Are you sure you want to kill all visible processes (y/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        StringBuilder messages = ProcessWatchApplication.KillProcesses(ProcessWatchApplication.TmpList);
                        _messenger.Append(messages);
                        pageNo = 1;
                        ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    }
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    _messenger.Append($"You have entered incorrect data ({input}).");
                    continue;
                }
            }
        }
        
        public void GetProcessesStartedAtDay(int pageSize, int pageNo, int day)
        {
            string patternDay = @"^--day=[0-3][0-9]$";
            Regex regxDay = new Regex(patternDay);
            
            string patternKill = @"^--kill=[0-9]+$";
            Regex regxKill = new Regex(patternKill);
            
            while (true)
            {
                var processesAtDate = ProcessWatchApplication.SelectProcessesStartAtDay(pageSize, pageNo, day);
                PrintProcessesView(processesAtDate.ProcessesList);
                
                Console.Write("PROCESSES FILTER BY DAY: ");
                int startPage = 1;
                if (processesAtDate.NumberOfPages == 0)
                {
                    Console.WriteLine($"Page 0 of {processesAtDate.NumberOfPages}");
                    startPage = 0;
                }
                else
                {
                    Console.WriteLine($"Page {pageNo} of {processesAtDate.NumberOfPages}");
                    startPage = 1;
                }

                Console.WriteLine("To go to the top menu write \"--gu\".");
                
                if (!string.IsNullOrEmpty(_messenger.ToString()))
                {
                    char[] charsToTrim = {' ', '\n', '\t'};
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(_messenger.ToString().Trim(charsToTrim));
                    _messenger.Clear();
                    Console.ResetColor();
                }
                
                Console.Write($"Enter the page number ({startPage} - {processesAtDate.NumberOfPages}) to go next page or write day of month (--day=DD): ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= processesAtDate.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You have entered the wrong page number ({number}).");
                        continue;
                    }
                }
                else if (input == "--rf")
                {
                    ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    Console.Clear();
                    continue;
                }
                else if (input == "--gu")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "--help")
                {
                    Console.Clear();
                    ViewHelper.HelpInfo();
                    continue;
                }
                else if (input == "--exit")
                {
                    Program.IsMainLoopRun = false;
                    break;
                }
                else if (!string.IsNullOrEmpty(input) && regxDay.IsMatch(input))
                {
                    var positionDay = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    int inputDay = Int32.Parse(input.Substring(positionDay));

                    if (inputDay >= 1 && inputDay <= 31)
                    {
                        day = inputDay;
                        pageNo = 1;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You entered an incorrect day of the month ({input.Substring(positionDay)}).");
                        continue;
                    }
                }
                else if (!string.IsNullOrEmpty(input) && regxKill.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string prosessIdStr = input.Substring(position);

                    if (Int32.TryParse(prosessIdStr , out var prosessId))
                    {
                        Console.Write("Are you sure you want to kill the process (y/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation == "y")
                        {
                            Console.Clear();
                            string message = ProcessWatchApplication.KillProcess(prosessId);
                            _messenger.Append($"{message}\n");
                            pageNo = 1;
                            ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"Process with an Id of {prosessIdStr} is not running.");
                    }
                }
                else if (input == "--kill-visible")
                {
                    Console.Write("Are you sure you want to kill all visible processes (y/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        StringBuilder messages = ProcessWatchApplication.KillProcesses(ProcessWatchApplication.TmpList);
                        _messenger.Append(messages);
                        pageNo = 1;
                        ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    }
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    _messenger.Append($"You have entered incorrect data ({input}).");
                    continue;
                }
            }
        }
        
        public void GetProcessesStartedAtMonth(int pageSize, int pageNo, int month)
        {
            string patternMonth = @"^--month=[0-1][0-9]$";
            Regex regxMonth = new Regex(patternMonth);
            
            string patternKill = @"^--kill=[0-9]+$";
            Regex regxKill = new Regex(patternKill);
            
            while (true)
            {
                var processesAtDate = ProcessWatchApplication.SelectProcessesStartAtMonth(pageSize, pageNo, month);
                PrintProcessesView(processesAtDate.ProcessesList);
                
                Console.Write("PROCESSES FILTER BY MONTH: ");
                int startPage = 1;
                if (processesAtDate.NumberOfPages == 0)
                {
                    Console.WriteLine($"Page 0 of {processesAtDate.NumberOfPages}");
                    startPage = 0;
                }
                else
                {
                    Console.WriteLine($"Page {pageNo} of {processesAtDate.NumberOfPages}");
                    startPage = 1;
                }

                Console.WriteLine("To go to the top menu write \"--gu\".");
                
                if (!string.IsNullOrEmpty(_messenger.ToString()))
                {
                    char[] charsToTrim = {' ', '\n', '\t'};
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(_messenger.ToString().Trim(charsToTrim));
                    _messenger.Clear();
                    Console.ResetColor();
                }
                
                Console.Write($"Enter the page number ({startPage} - {processesAtDate.NumberOfPages}) to go next page or write a month (--month=MM): ");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out var number))
                {
                    if (number >= 1 && number <= processesAtDate.NumberOfPages)
                    {
                        pageNo = number;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You have entered the wrong  page number ({number}).");
                        continue;
                    }
                }
                else if (input == "--rf")
                {
                    ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    Console.Clear();
                    continue;
                }
                else if (input == "--gu")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "--help")
                {
                    Console.Clear();
                    ViewHelper.HelpInfo();
                    continue;
                }
                else if (input == "--exit")
                {
                    Program.IsMainLoopRun = false;
                    break;
                }
                else if (!string.IsNullOrEmpty(input) && regxMonth.IsMatch(input))
                {
                    var positionMonth = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    int inputMonth = Int32.Parse(input.Substring(positionMonth));

                    if (inputMonth >= 1 && inputMonth <= 12)
                    {
                        month = inputMonth;
                        pageNo = 1;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"You entered an incorrect month ({input.Substring(positionMonth)}).");
                        continue;
                    }
                }
                else if (!string.IsNullOrEmpty(input) && regxKill.IsMatch(input))
                {
                    var position = input.IndexOf("=", StringComparison.Ordinal) + 1;
                    string prosessIdStr = input.Substring(position);

                    if (Int32.TryParse(prosessIdStr , out var prosessId))
                    {
                        Console.Write("Are you sure you want to kill the process (y/n): ");
                        string confirmation = Console.ReadLine();

                        if (confirmation == "y")
                        {
                            Console.Clear();
                            string message = ProcessWatchApplication.KillProcess(prosessId);
                            _messenger.Append($"{message}\n");
                            pageNo = 1;
                            ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                        }
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        _messenger.Append($"Process with an Id of {prosessIdStr} is not running.");
                    }
                }
                else if (input == "--kill-visible")
                {
                    Console.Write("Are you sure you want to kill all visible processes (y/n): ");
                    string confirmation = Console.ReadLine();

                    if (confirmation == "y")
                    {
                        Console.Clear();
                        StringBuilder messages = ProcessWatchApplication.KillProcesses(ProcessWatchApplication.TmpList);
                        _messenger.Append(messages);
                        pageNo = 1;
                        ProcessWatchApplication.RefreshAllMemoryItemProcesses();
                    }
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    _messenger.Append($"You have entered incorrect data ({input}).");
                    continue;
                }
            }
        }

        private void PrintProcessesView(List<MemoryItemProcess> processesList)
        {
            StringBuilder sb = new StringBuilder();

            string line = new String('-', 145);

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
        }
    }
}