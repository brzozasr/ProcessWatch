using System;
using System.Collections.Generic;
using Codecool.ProcessWatch.Controller;
using Codecool.ProcessWatch.Model;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class Gui : Window
    {
        private NodeStore _store;
        private const int PageSize = 25;
        private List<MemoryItemProcess> _processesList = new List<MemoryItemProcess>();
        private int _numberOfPages;
        private int _pageNo;
        private readonly Label _pageNoLbl;
        private readonly Button _previousBtn;
        private readonly Button _nextBtn;

        public Gui() : base("Process Watch")
        {
            SetPosition(WindowPosition.Center);
            SetSizeRequest(800, 500);
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

            _previousBtn = new Button("<<");
            _pageNoLbl = new Label($"{_pageNo} of {_numberOfPages}");
            _pageNoLbl.SetSizeRequest(60, 30);
            _nextBtn = new Button(">>");

            if (_pageNo <= 1 && _numberOfPages <= 1)
            {
                _previousBtn.Sensitive = false;
                _nextBtn.Sensitive = false;
            }
            else if (_pageNo == 1)
            {
                _previousBtn.Sensitive = false;
            }
            else if (_pageNo == _numberOfPages)
            {
                _nextBtn.Sensitive = false;
            }

            _previousBtn.Clicked += OnClickPreviousBtn;
            _nextBtn.Clicked += OnClickNextBtn;

            naviBtnHBox.Add(_previousBtn);
            naviBtnHBox.Add(_pageNoLbl);
            naviBtnHBox.Add(_nextBtn);

            Alignment naviBtnAlignment = new Alignment(0.5f, 0, 0, 0);
            naviBtnAlignment.Add(naviBtnHBox);

            mainVBox.PackStart(view, true, true, 0);
            mainVBox.PackEnd(naviBtnAlignment, true, true, 20);

            Add(mainVBox);
            ShowAll();
        }

        private void OnClickPreviousBtn(object sender, EventArgs e)
        {
            RemoveAllDataView();
            _pageNo -= 1;
            _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            RefreshView();
            SetNaviBtnSensitive();
        }

        private void OnClickNextBtn(object sender, EventArgs e)
        {
            RemoveAllDataView();
            _pageNo += 1;
            _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            RefreshView();
            SetNaviBtnSensitive();
        }

        private void SetNaviBtnSensitive()
        {
            if (_pageNo <= 1 && _numberOfPages <= 1)
            {
                _previousBtn.Sensitive = false;
                _nextBtn.Sensitive = false;
            }
            else if (_pageNo == 1)
            {
                _previousBtn.Sensitive = false;
            }
            else if (_pageNo == _numberOfPages)
            {
                _nextBtn.Sensitive = false;
            }
            else
            {
                _previousBtn.Sensitive = true;
                _nextBtn.Sensitive = true;
            }
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
            if (_processesList.Count == 0)
            {
                _pageNo = 1;
                var processesData = ProcessWatchApplication.AllProcesses(PageSize, _pageNo);
                _processesList = processesData.ProcessesList;
                _numberOfPages = processesData.NumberOfPages;
            }

            if (_store == null)
            {
                _store = new NodeStore(typeof(RowData));


                foreach (var p in _processesList)
                {
                    _store.AddNode(new RowData(
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

            return _store;
        }

        private void RemoveAllDataView()
        {
            _store.Clear();
        }

        private void RefreshView()
        {
            _processesList = ProcessWatchApplication.AllProcesses(PageSize, _pageNo).ProcessesList;
            
            foreach (var p in _processesList)
            {
                _store.AddNode(new RowData(
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
    }
}