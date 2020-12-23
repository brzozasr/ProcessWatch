using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Codecool.ProcessWatch.Model.Utilities;

namespace Codecool.ProcessWatch.Model
{
    /// <summary>
    /// Model of the memory processes
    /// </summary>
    public class MemoryItemProcess
    {
        public int? ProcessId { get; }
        public string ProcessName { get; }
        public long? PhysicalMemoryUsage { get; } // long, Physical memory usage {myProcess.WorkingSet64}
        public string PriorityClass { get; } // ProcessPriorityClass, Priority class {myProcess.PriorityClass}
        public double? UserProcessorTime { get; } // TimeSpan, User processor time {myProcess.UserProcessorTime}
        public double? PrivilegedProcessorTime { get; } // TimeSpan, Privileged processor time {PrivilegedProcessorTime}
        public double? TotalProcessorTime { get; } // TimeSpan,  Total processor time {myProcess.TotalProcessorTime}
        public int? ThreadsNumber { get; }
        public int? BasePriority { get; } // int, Base priority {myProcess.BasePriority}
        public double? PagedSystemMemorySize { get; } // int, Paged system memory size {myProcess.PagedSystemMemorySize64}
        public double? PagedMemorySize { get; } // int, Paged memory size {myProcess.PagedMemorySize64}
        public double? PeakPhysicalMemoryUsage { get; } // int,  Peak physical memory usage {peakWorkingSet}
        public double? PeakPagedMemorySize { get; } // int, Peak paged memory usage {PeakPagedMemorySize}
        public string StartInfoUserName { get; }
        
        public MemoryItemProcess(int processId)
        {
            ProcessId = GetProcessId(processId); //
            ProcessName = GetProcessName(processId); //
            PhysicalMemoryUsage = GetPhysicalMemoryUsage(processId); //
            PriorityClass = GetPriorityClass(processId); //
            UserProcessorTime = GetUserProcessorTime(processId); //
            PrivilegedProcessorTime = GetPrivilegedProcessorTime(processId); //
            TotalProcessorTime = GetTotalProcessorTime(processId); //
            ThreadsNumber = GetThreadsNumber(processId);
            BasePriority = GetBasePriority(processId); //
            PagedSystemMemorySize = GetPagedSystemMemorySize(processId); //
            PagedMemorySize = GetPagedMemorySize(processId); //
            PeakPhysicalMemoryUsage = GetPeakPhysicalMemoryUsage(processId); //
            PeakPagedMemorySize = GetPeakPagedMemorySize(processId); //
            StartInfoUserName = GetStartInfoUserName(processId); //
        }
    }
}