using System;
using System.Diagnostics;

namespace Codecool.ProcessWatch.Model
{
    public static class Utilities
    {
        internal static string GetProcessId(int id)
        {
            try
            {
                return id.ToString();
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetProcessName(int id)
        {
            try
            {
                string name = Process.GetProcessById(id).ProcessName;

                if (string.IsNullOrEmpty(name))
                {
                    return "N/A";
                }
                else
                {
                    return name;
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPhysicalMemoryUsage(int id)
        {
            try
            {
                return Process.GetProcessById(id).WorkingSet64.ToString();
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetBasePriority(int id)
        {
            try
            {
                int basePriority = Process.GetProcessById(id).BasePriority;
                return basePriority.ToString();
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPriorityClass(int id)
        {
            try
            {
                ProcessPriorityClass priorityClass = Process.GetProcessById(id).PriorityClass;
                return priorityClass.ToString();
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetUserProcessorTime(int id)
        {
            try
            {
                TimeSpan userProcessorTime = Process.GetProcessById(id).UserProcessorTime;
                double time = userProcessorTime.TotalMilliseconds;
                if (time >= 1000)
                {
                    return (time / 1000).ToString("0.## s");
                }
                else
                {
                    return time.ToString("0.## ms");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPrivilegedProcessorTime(int id)
        {
            try
            {
                TimeSpan privilegedProcessorTime = Process.GetProcessById(id).PrivilegedProcessorTime;
                double time = privilegedProcessorTime.TotalMilliseconds;
                if (time >= 1000)
                {
                    return (time / 1000).ToString("0.## s");
                }
                else
                {
                    return time.ToString("0.## ms");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetTotalProcessorTime(int id)
        {
            try
            {
                TimeSpan totalProcessorTime = Process.GetProcessById(id).TotalProcessorTime;
                double time = totalProcessorTime.TotalMilliseconds;
                if (time >= 1000)
                {
                    return (time / 1000).ToString("0.## s");
                }
                else
                {
                    return time.ToString("0.## ms");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPagedSystemMemorySize(int id)
        {
            try
            {
                long pagedSystemMemorySize = Process.GetProcessById(id).PagedSystemMemorySize64;

                if (pagedSystemMemorySize >= 1024 * 1024)
                {
                    return (pagedSystemMemorySize / 1024 / 1024).ToString("0.## MB");
                }
                else if (pagedSystemMemorySize >= 1024)
                {
                    return (pagedSystemMemorySize / 1024).ToString("0.## kB");
                }
                else
                {
                    return pagedSystemMemorySize.ToString("0 B");
                }
                
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPagedMemorySize(int id)
        {
            try
            {
                long pagedMemorySize = Process.GetProcessById(id).PagedMemorySize64;
                
                if (pagedMemorySize >= 1024 * 1024)
                {
                    return (pagedMemorySize / 1024 / 1024).ToString("0.## MB");
                }
                else if (pagedMemorySize >= 1024)
                {
                    return (pagedMemorySize / 1024).ToString("0.## kB");
                }
                else
                {
                    return pagedMemorySize.ToString("0 B");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPeakPhysicalMemoryUsage(int id)
        {
            try
            {
                long peakPhysicalMemoryUsage = Process.GetProcessById(id).PeakWorkingSet64;
                
                if (peakPhysicalMemoryUsage >= 1024 * 1024)
                {
                    return (peakPhysicalMemoryUsage / 1024 / 1024).ToString("0.## MB");
                }
                else if (peakPhysicalMemoryUsage >= 1024)
                {
                    return (peakPhysicalMemoryUsage / 1024).ToString("0.## kB");
                }
                else
                {
                    return peakPhysicalMemoryUsage.ToString("0 B");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetPeakPagedMemorySize(int id)
        {
            try
            {
                long peakPagedMemorySize = Process.GetProcessById(id).PeakPagedMemorySize64;
                
                if (peakPagedMemorySize >= 1024 * 1024)
                {
                    return (peakPagedMemorySize / 1024 / 1024).ToString("0.## MB");
                }
                else if (peakPagedMemorySize >= 1024)
                {
                    return (peakPagedMemorySize / 1024).ToString("0.## kB");
                }
                else
                {
                    return peakPagedMemorySize.ToString("0 B");
                }
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetStartInfoUserName(int id)
        {
            try
            {
                string name = Process.GetProcessById(id).StartInfo.UserName;
                return name;
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }
        
        internal static string GetThreadsNumber(int id)
        {
            try
            {
                int threadsCount = Process.GetProcessById(id).Threads.Count;
                return threadsCount.ToString();
            }
            catch (Exception)
            {
                // ignored
                return "N/A";
            }
        }

        internal static string ByteConverter(double? noByte)
        {
            if (noByte.HasValue)
            {
                if (noByte.Value >= 1024 * 1024)
                {
                    return (noByte.Value / 1024 / 1024).ToString("0.## MB");
                }
                else if (noByte.Value >= 1024)
                {
                    return (noByte.Value / 1024).ToString("0.## kB");
                }
                else
                {
                    return noByte.Value.ToString("0 Byte");
                }
            }

            return "N/A";
        }
    }
}
