using System;

namespace Codecool.ProcessWatch.Controller
{
    public static class Converters
    {
        internal static string ByteConverter(double? totalBytes)
        {
            if (totalBytes.HasValue)
            {
                if (totalBytes.Value >= 1024 * 1024 * 1024)
                {
                    return (totalBytes.Value / 1024 / 1024 / 1024).ToString("0.## GB");
                }
                else if (totalBytes.Value >= 1024 * 1024)
                {
                    return (totalBytes.Value / 1024 / 1024).ToString("0.## MB");
                }
                else if (totalBytes.Value >= 1024)
                {
                    return (totalBytes.Value / 1024).ToString("0.## kB");
                }
                else
                {
                    return totalBytes.Value.ToString("0 Byte");
                }
            }

            return "N/A";
        }

        internal static string TimeConverter(double? totalMilliseconds)
        {
            if (totalMilliseconds.HasValue)
            {
                if (totalMilliseconds.Value >= 1000)
                {
                    return (totalMilliseconds.Value / 1000).ToString("0.## s");
                }
                else
                {
                    return totalMilliseconds.Value.ToString("0.## ms");
                }
            }

            return "N/A";
        }

        internal static string StrNullConverter(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "N/A";
            }

            return str;
        }
        
        internal static string IntNullConverter(int? value)
        {
            if (value.HasValue)
            {
                return value.ToString();
            }

            return "N/A";
        }
        
        internal static string DateTimeNullConverter(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.ToString();
            }

            return "N/A";
        }
    }
}