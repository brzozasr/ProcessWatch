using System;
using Codecool.ProcessWatch.Controller;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class Gui : Window
    {
        Gtk.NodeStore store;

        Gtk.NodeStore Store
        {
            get
            {
                if (store == null)
                {
                    int pageSize = 20;
                    store = new Gtk.NodeStore(typeof(ColumnName));
                    
                    foreach (var p in ProcessWatchApplication.AllProcesses(pageSize, 1).ProcessesList)
                    {
                        store.AddNode(new ColumnName(
                            Converters.IntNullConverter(p.ProcessId), 
                            Converters.StrNullConverter(p.ProcessName),
                        Converters.ByteConverter(p.PhysicalMemoryUsage),
                            Converters.StrNullConverter(p.PriorityClass),
                            Converters.TimeConverter(p.UserProcessorTime),
                            Converters.TimeConverter(p.PrivilegedProcessorTime),
                            Converters.TimeConverter(p.TotalProcessorTime),
                            Converters.IntNullConverter(p.ThreadsNumber),
                            Converters.DateTimeNullConverter(p.StartTime),
                            Converters.IntNullConverter(p.BasePriority)));
                    }
                }

                return store;
            }
        }

        public Gui() : base("Process Watch")
        {
            SetPosition(WindowPosition.Center);
            SetSizeRequest(750, 500);
            this.TypeHint = Gdk.WindowTypeHint.Normal;
            this.Resizable = true;
            DeleteEvent += delegate { Application.Quit(); };

            VBox mainVBox = new VBox(true, 0);

            NodeView view = new NodeView(Store);
            view.AppendColumn("PID", new Gtk.CellRendererText(), "text", 0);
            view.AppendColumn("Name", new Gtk.CellRendererText(), "text", 1);
            view.AppendColumn("Memory Usage", new Gtk.CellRendererText(), "text", 2);
            view.AppendColumn("Priority", new Gtk.CellRendererText(), "text", 3);
            view.AppendColumn("User CPU Time", new Gtk.CellRendererText(), "text", 4);
            view.AppendColumn("Privileged CPU Time", new Gtk.CellRendererText(), "text", 5);
            view.AppendColumn("Total CPU Time", new Gtk.CellRendererText(), "text", 6);
            view.AppendColumn("Threads", new Gtk.CellRendererText(), "text", 7);
            view.AppendColumn("Start Time", new Gtk.CellRendererText(), "text", 8);
            view.AppendColumn("Base Priority", new Gtk.CellRendererText(), "text", 9);
            view.Show();

            mainVBox.PackStart(view, true, true, 0);

            Add(mainVBox);
            ShowAll();
        }

        [Gtk.TreeNode(ListOnly = true)]
        public class ColumnName : Gtk.TreeNode
        {
            [field: Gtk.TreeNodeValue(Column = 0)] public string ProcessId;

            [Gtk.TreeNodeValue(Column = 1)] 
            public string ProcessTitle { get; }
            
            [Gtk.TreeNodeValue(Column = 2)]
            public string MemoryUsage { get; }
            
            [Gtk.TreeNodeValue(Column = 3)]
            public string Priority { get; }
            
            [Gtk.TreeNodeValue(Column = 4)]
            public string UserCpuTime { get; }
            
            [Gtk.TreeNodeValue(Column = 5)]
            public string PrivilegedCpuTime { get; }
            
            [Gtk.TreeNodeValue(Column = 6)]
            public string TotalCpuTime { get; }
            
            [Gtk.TreeNodeValue(Column = 7)]
            public string ThreadsNo { get; }
            
            [Gtk.TreeNodeValue(Column = 8)]
            public string StartedTime { get; }
            
            [Gtk.TreeNodeValue(Column = 9)]
            public string BasePriorityNo { get; }

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
}