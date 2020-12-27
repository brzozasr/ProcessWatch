using System;
using System.Collections.Generic;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class Gui : Window
    {
        NodeStore store;
        private const int PageSize = 25;
        private List<MemoryItemProcess> _processesList = new List<MemoryItemProcess>();
        private int _numberOfPages = 0;
        

        /*NodeStore Store
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
        }*/

        public Gui() : base("Process Watch")
        {
            SetPosition(WindowPosition.Center);
            SetSizeRequest(750, 500);
            // this.TypeHint = Gdk.WindowTypeHint.Normal;
            this.Resizable = true;

            DeleteEvent += AppQuit;

            CellRendererText rightAlignment = new CellRendererText();
            rightAlignment.SetAlignment(1.0f, 1.0f);
            
            CellRendererText centerAlignment = new CellRendererText();
            centerAlignment.SetAlignment(0.5f, 0.5f);

            CellRendererText leftAlignment = new CellRendererText();
            leftAlignment.SetAlignment(0.0f, 0.0f);
            
            NodeView view = new NodeView(GetNodeStoreStore());
            view.AppendColumn("PID", rightAlignment, "text", 0);
            view.AppendColumn("Name", leftAlignment, "text", 1);
            view.AppendColumn("Memory Usage", rightAlignment, "text", 2);
            view.AppendColumn("Priority", leftAlignment, "text", 3);
            view.AppendColumn("User CPU Time", rightAlignment, "text", 4);
            view.AppendColumn("Privileged CPU Time", rightAlignment, "text", 5);
            view.AppendColumn("Total CPU Time", rightAlignment, "text", 6);
            view.AppendColumn("Threads", rightAlignment, "text", 7);
            view.AppendColumn("Start Time", centerAlignment, "text", 8);
            view.AppendColumn("Base Priority", rightAlignment, "text", 9);

            view.Show();

            // loop for updating GUI
            // while (Gtk.Application.EventsPending())
            // {
            //     Gtk.Application.RunIteration();
            // }
            
            VBox mainVBox = new VBox(false, 0);
            HBox naviBtnHBox = new HBox(false, 10);

            Button previousBtn = new Button("<<");
            Label pageNoLbl = new Label($"1 of {_numberOfPages}");
            pageNoLbl.SetSizeRequest(60, 30);
            Button nextBtn = new Button(">>");

            naviBtnHBox.Add(previousBtn);
            naviBtnHBox.Add(pageNoLbl);
            naviBtnHBox.Add(nextBtn);

            Alignment naviBtnAlignment = new Alignment(0.5f, 0, 0, 0);
            naviBtnAlignment.Add(naviBtnHBox);

            mainVBox.PackStart(view, true, true, 0);
            mainVBox.PackEnd(naviBtnAlignment, true, true, 20);

            Add(mainVBox);
            ShowAll();
        }

        private void AppQuit(object o, DeleteEventArgs args)
        {
            Gtk.Application.Invoke(delegate
            {
                Application.Quit();
                Dispose();
            });
        }

        private NodeStore GetNodeStoreStore()
        {
            _processesList = ProcessWatchApplication.TmpList;
            _numberOfPages = ProcessWatchApplication.TmpNumberOfPages;

            if (_processesList.Count == 0)
            {
                _processesList = ProcessWatchApplication.AllProcesses(PageSize, 1).ProcessesList;
            }

            if (store == null)
            {
                store = new NodeStore(typeof(RowData));


                foreach (var p in _processesList)
                {
                    store.AddNode(new RowData(
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
}