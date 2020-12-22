using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public string
            PagedSystemMemorySize { get; } // int, Paged system memory size {myProcess.PagedSystemMemorySize64}

        public string PagedMemorySize { get; } // int, Paged memory size {myProcess.PagedMemorySize64}
        public string PeakPhysicalMemoryUsage { get; } // int,  Peak physical memory usage {peakWorkingSet}
        public string PeakPagedMemorySize { get; } // int, Peak paged memory usage {PeakPagedMemorySize}
        private List<MemoryItemProcess> prosessesList = new List<MemoryItemProcess>();

        public MemoryItemProcess()
        {
        }

        private MemoryItemProcess(int processId, string processName, long physicalMemoryUsage, int basePriority,
            string priorityClass, string userProcessorTime, TimeSpan privilegedProcessorTime,
            TimeSpan totalProcessorTime, long pagedSystemMemorySize, long pagedMemorySize, long peakPhysicalMemoryUsage,
            long peakPagedMemorySize)
        {
            ProcessId = processId.ToString();
            ProcessName = processName;
            PhysicalMemoryUsage = physicalMemoryUsage.ToString();
            BasePriority = basePriority.ToString();
            PriorityClass = priorityClass.ToString();
            UserProcessorTime = userProcessorTime.ToString();
            PrivilegedProcessorTime = privilegedProcessorTime.ToString();
            TotalProcessorTime = totalProcessorTime.ToString();
            PagedSystemMemorySize = pagedSystemMemorySize.ToString();
            PagedMemorySize = pagedMemorySize.ToString();
            PeakPhysicalMemoryUsage = peakPhysicalMemoryUsage.ToString();
            PeakPagedMemorySize = peakPagedMemorySize.ToString();
        }

        public List<MemoryItemProcess> GetMemoryItemProcessList()
        {
            string priorityClass;
            string userProcessorTime;
            
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    priorityClass = process.PriorityClass.ToString();
                }
                catch (Exception e)
                {
                    // ignored
                    priorityClass = "N/A";
                }
                
                try
                {
                    userProcessorTime = process.UserProcessorTime.ToString();
                }
                catch (Exception e)
                {
                    // ignored
                    userProcessorTime = "N/A";
                }

                try
                {
                    prosessesList.Add(new MemoryItemProcess(
                        process.Id,
                        process.ProcessName,
                        process.WorkingSet64,
                        process.BasePriority,
                        // process.PriorityClass,
                        priorityClass,
                        // process.UserProcessorTime,
                        userProcessorTime,
                        process.PrivilegedProcessorTime,
                        process.TotalProcessorTime,
                        process.PagedSystemMemorySize64,
                        process.PagedMemorySize64,
                        process.PeakWorkingSet64,
                        process.PeakPagedMemorySize64));
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            return prosessesList;
        }
    }
}