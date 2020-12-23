using System;
using System.Diagnostics;

namespace Codecool.ProcessWatch.Model
{
    public static class Utilities
    {
        internal static int? GetProcessId(int id)
        {
            try
            {
                return id;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static string GetProcessName(int id)
        {
            try
            {
                string name = Process.GetProcessById(id).ProcessName;

                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                else
                {
                    return name;
                }
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static long? GetPhysicalMemoryUsage(int id)
        {
            try
            {
                return Process.GetProcessById(id).WorkingSet64;
            }
            catch (Exception)
            {
                // ignored
                return null;
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
                return null;
            }
        }
        
        internal static double? GetUserProcessorTime(int id)
        {
            try
            {
                TimeSpan userProcessorTime = Process.GetProcessById(id).UserProcessorTime;
                return userProcessorTime.TotalMilliseconds;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static double? GetPrivilegedProcessorTime(int id)
        {
            try
            {
                TimeSpan privilegedProcessorTime = Process.GetProcessById(id).PrivilegedProcessorTime;
                return privilegedProcessorTime.TotalMilliseconds;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static double? GetTotalProcessorTime(int id)
        {
            try
            {
                TimeSpan totalProcessorTime = Process.GetProcessById(id).TotalProcessorTime;
                return totalProcessorTime.TotalMilliseconds;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }

        internal static string GetStartInfoUserName(int id)
        {
            try
            {
                return Process.GetProcessById(id).StartInfo.UserName;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static int? GetThreadsNumber(int id)
        {
            try
            {
                return Process.GetProcessById(id).Threads.Count;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        internal static int? GetBasePriority(int id)
        {
            try
            {
                return Process.GetProcessById(id).BasePriority;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        /// <summary>
        /// Paged System Memory (MacOS = 0 Byte)
        /// </summary>
        /// <param name="id">Process ID</param>
        /// <returns></returns>
        internal static long? GetPagedSystemMemorySize(int id)
        {
            try
            {
                return Process.GetProcessById(id).PagedSystemMemorySize64;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        /// <summary>
        /// Paged Memory Size (MacOS = 0 Byte)
        /// </summary>
        /// <param name="id">Process ID</param>
        /// <returns></returns>
        internal static long? GetPagedMemorySize(int id)
        {
            try
            {
                return Process.GetProcessById(id).PagedMemorySize64;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        /// <summary>
        /// Peak Physical Memory Usage (MacOS = 0 Byte)
        /// </summary>
        /// <param name="id">Process ID</param>
        /// <returns></returns>
        internal static long? GetPeakPhysicalMemoryUsage(int id)
        {
            try
            {
                return Process.GetProcessById(id).PeakWorkingSet64;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
        
        /// <summary>
        /// Peak Paged Memory Size (MacOS = 0 Byte)
        /// </summary>
        /// <param name="id">Process ID</param>
        /// <returns></returns>
        internal static long? GetPeakPagedMemorySize(int id)
        {
            try
            {
                return Process.GetProcessById(id).PeakPagedMemorySize64;
            }
            catch (Exception)
            {
                // ignored
                return null;
            }
        }
    }
}
