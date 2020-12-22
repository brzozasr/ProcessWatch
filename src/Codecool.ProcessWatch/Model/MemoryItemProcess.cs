using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Codecool.ProcessWatch.Model.Utilities;

namespace Codecool.ProcessWatch.Model
{
    public class MemoryItemProcess
    {
        public string ProcessId { get; }
        public string ProcessName { get; } // string
        public string PhysicalMemoryUsage { get; } // long, Physical memory usage {myProcess.WorkingSet64}
        public string BasePriority { get; } // int, Base priority {myProcess.BasePriority}
        public string PriorityClass { get; } // ProcessPriorityClass, Priority class {myProcess.PriorityClass}
        public string UserProcessorTime { get; } // TimeSpan, User processor time {myProcess.UserProcessorTime}
        public string PrivilegedProcessorTime { get; } // TimeSpan, Privileged processor time {PrivilegedProcessorTime}
        public string TotalProcessorTime { get; } // TimeSpan,  Total processor time {myProcess.TotalProcessorTime}
        public string PagedSystemMemorySize { get; } // int, Paged system memory size {myProcess.PagedSystemMemorySize64}
        public string PagedMemorySize { get; } // int, Paged memory size {myProcess.PagedMemorySize64}
        public string PeakPhysicalMemoryUsage { get; } // int,  Peak physical memory usage {peakWorkingSet}
        public string PeakPagedMemorySize { get; } // int, Peak paged memory usage {PeakPagedMemorySize}
        public string StartInfoUserName { get; }
        public string ThreadsNumber { get; }
        
        private List<MemoryItemProcess> prosessesList = new List<MemoryItemProcess>();

        public MemoryItemProcess(int processId)
        {
            ProcessId = GetProcessId(processId);
            ProcessName = GetProcessName(processId);
            PhysicalMemoryUsage = GetPhysicalMemoryUsage(processId);
            BasePriority = GetBasePriority(processId);
            PriorityClass = GetPriorityClass(processId);
            UserProcessorTime = GetUserProcessorTime(processId);
            PrivilegedProcessorTime = GetPrivilegedProcessorTime(processId);
            TotalProcessorTime = GetTotalProcessorTime(processId);
            PagedSystemMemorySize = GetPagedSystemMemorySize(processId);
            PagedMemorySize = GetPagedMemorySize(processId);
            PeakPhysicalMemoryUsage = GetPeakPhysicalMemoryUsage(processId);
            PeakPagedMemorySize = GetPeakPagedMemorySize(processId);
            StartInfoUserName = GetStartInfoUserName(processId);
            ThreadsNumber = GetThreadsNumber(processId);
        }
    }
}