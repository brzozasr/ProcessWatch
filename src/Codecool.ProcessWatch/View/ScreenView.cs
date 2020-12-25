namespace Codecool.ProcessWatch.View
{
    public class ScreenView
    {
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

        var pagination = ProcessWatchApplication.SelectProcessesByNamePagination(pageSize, 1, "we");

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
