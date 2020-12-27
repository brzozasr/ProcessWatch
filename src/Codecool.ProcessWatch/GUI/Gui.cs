using System;
using Codecool.ProcessWatch.Controller;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class Gui : Window
    {
        NodeStore store;

        NodeStore Store
        {
            get
            {
                if (store == null)
                {
                    int pageSize = 25;
                    store = new NodeStore(typeof(ColumnName));
                    
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
            view.AppendColumn("PID", new CellRendererText(), "text", 0);
            view.AppendColumn("Name", new CellRendererText(), "text", 1);
            view.AppendColumn("Memory Usage", new CellRendererText(), "text", 2);
            view.AppendColumn("Priority", new CellRendererText(), "text", 3);
            view.AppendColumn("User CPU Time", new CellRendererText(), "text", 4);
            view.AppendColumn("Privileged CPU Time", new CellRendererText(), "text", 5);
            view.AppendColumn("Total CPU Time", new CellRendererText(), "text", 6);
            view.AppendColumn("Threads", new CellRendererText(), "text", 7);
            view.AppendColumn("Start Time", new CellRendererText(), "text", 8);
            view.AppendColumn("Base Priority", new CellRendererText(), "text", 9);
            view.Show();

            mainVBox.PackStart(view, true, true, 0);

            Add(mainVBox);
            ShowAll();
        }
    }
}