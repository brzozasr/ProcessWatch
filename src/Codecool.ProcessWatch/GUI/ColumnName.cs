namespace Codecool.ProcessWatch.GUI
{
    public class ColumnName : Gtk.TreeNode
    {
        [field: Gtk.TreeNodeValue(Column = 0)] public string ProcessId;

        [Gtk.TreeNodeValue(Column = 1)] public string ProcessTitle { get; }

        [Gtk.TreeNodeValue(Column = 2)] public string MemoryUsage { get; }

        [Gtk.TreeNodeValue(Column = 3)] public string Priority { get; }

        [Gtk.TreeNodeValue(Column = 4)] public string UserCpuTime { get; }

        [Gtk.TreeNodeValue(Column = 5)] public string PrivilegedCpuTime { get; }

        [Gtk.TreeNodeValue(Column = 6)] public string TotalCpuTime { get; }

        [Gtk.TreeNodeValue(Column = 7)] public string ThreadsNo { get; }

        [Gtk.TreeNodeValue(Column = 8)] public string StartedTime { get; }

        [Gtk.TreeNodeValue(Column = 9)] public string BasePriorityNo { get; }

        public ColumnName(string processId, string processTitle, string memoryUsage,
            string priority, string userCpuTime, string privilegedCpuTime, string totalCpuTime,
            string threadsNo, string startedTime, string basePriorityNo)
        {
            ProcessId = processId;
            ProcessTitle = processTitle;
            MemoryUsage = memoryUsage;
            Priority = priority;
            UserCpuTime = userCpuTime;
            PrivilegedCpuTime = privilegedCpuTime;
            TotalCpuTime = totalCpuTime;
            ThreadsNo = threadsNo;
            StartedTime = startedTime;
            BasePriorityNo = basePriorityNo;
        }
    }
}