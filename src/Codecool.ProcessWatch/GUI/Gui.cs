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
        private readonly ComboBox _filterCb;
        private readonly string[] _cbStore;
        private readonly HBox _topRightHBox;
        private Entry _searchEntry;
        private SpinButton _startAtDateDaySb;
        private SpinButton _startAtDateMonthSb;
        private SpinButton _startAtDateYearSb;
        private SpinButton _startAtDaySb;
        private SpinButton _startAtMonthSb;

        public Gui() : base("Process Watch - GUI")
        {
            _cbStore = new[]
            {
                "- processes show all",
                "- processes name",
                "- processes started at date",
                "- processes started at day",
                "- processes started at month",
                "- processes started before date",
                "- processes started after date",
                "- processes physical memory usage greater than...",
                "- processes user CPU time greater than...",
                "- processes total CPU time greater than..."
            };

            SetPosition(WindowPosition.Center);
            SetSizeRequest(960, 550);
            this.TypeHint = Gdk.WindowTypeHint.Desktop;
            this.Resizable = false;

            DeleteEvent += AppQuit;

            ScrolledWindow scrolled = new ScrolledWindow();
            scrolled.HeightRequest = 480;

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
            // view.Show();
            
            scrolled.Add(view);

            VBox mainVBox = new VBox(false, 0);
            HBox topHBox = new HBox(false, 20);
            VBox topLeftVBox = new VBox(false, 0);
            _topRightHBox = new HBox(false, 0);
            HBox naviBtnHBox = new HBox(false, 10);

            Label filterLbl = new Label("Filter by:");
            _filterCb = new ComboBox(_cbStore);
            _filterCb.Active = 0;
            Label emptyLbl = new Label();
            
            Fixed topLeftFixed = new Fixed();

            topLeftFixed.Put(filterLbl, 10, 10);
            topLeftFixed.Put(_filterCb, 10, 30);
            topLeftFixed.Put(emptyLbl, 10, 65);

            topLeftVBox.Add(topLeftFixed);

            // _searchEntry = new Entry();
            // topRightHBox.Add(_searchEntry);

            Alignment topRightAlignment = new Alignment(0, 0.7f, 0, 0);
            topRightAlignment.Add(_topRightHBox);

            topHBox.PackStart(topLeftVBox, false, false, 0);
            topHBox.PackEnd(topRightAlignment, true, true, 0);

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
            _filterCb.Changed += OnChanged;

            naviBtnHBox.Add(_previousBtn);
            naviBtnHBox.Add(_pageNoLbl);
            naviBtnHBox.Add(_nextBtn);

            Alignment naviBtnAlignment = new Alignment(0.5f, 0, 0, 0);
            naviBtnAlignment.Add(naviBtnHBox);

            mainVBox.PackStart(topHBox, true, true, 0);
            mainVBox.PackStart(scrolled, true, true, 0);
            mainVBox.PackEnd(naviBtnAlignment, true, true, 20);

            Add(mainVBox);
            ShowAll();
            
            // loop for updating GUI
            // while (Gtk.Application.EventsPending())
            // {
            //     Gtk.Application.RunIteration();
            // }
        }

        private void CreateHidenWidgets()
        {
            _searchEntry = new Entry();
            _searchEntry.Visible = false;
            _topRightHBox.Add(_searchEntry);

            _startAtDateDaySb = new SpinButton(1, 31, 1);
            _startAtDateDaySb.Value = DateTime.Now.Day;
            _startAtDateDaySb.Visible = false;
            _startAtDateMonthSb = new SpinButton(1, 12, 1);
            _startAtDateMonthSb.Value = DateTime.Now.Month;
            _startAtDateMonthSb.Visible = false;
            _startAtDateYearSb = new SpinButton(1990, 2200, 1);
            _startAtDateYearSb.Value = DateTime.Now.Year;
            _startAtDateYearSb.Visible = false;
            _topRightHBox.Add(_startAtDateDaySb);
            _topRightHBox.Add(_startAtDateMonthSb);
            _topRightHBox.Add(_startAtDateYearSb);
            
            _startAtDaySb = new SpinButton(1, 31, 1);
            _startAtDaySb.Value = DateTime.Now.Day;
            _startAtDaySb.Visible = false;
            _topRightHBox.Add(_startAtDaySb);
            
            _startAtMonthSb = new SpinButton(1, 12, 1);
            _startAtMonthSb.Value = DateTime.Now.Month;
            _startAtMonthSb.Visible = false;
            _topRightHBox.Add(_startAtMonthSb);
        }

        private void OnChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox) sender;

            if (_searchEntry == null)
            {
                CreateHidenWidgets();
            }

            switch (_cbStore[cb.Active])
            {
                case "- processes show all":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes name":
                    _searchEntry.Visible = true;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes started at date":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = true;
                    _startAtDateMonthSb.Visible = true;
                    _startAtDateYearSb.Visible = true;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes started at day":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = true;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes started at month":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = true;
                    break;
                case "- processes started before date":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes started after date":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes physical memory usage greater than...":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes user CPU time greater than...":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                case "- processes total CPU time greater than...":
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
                default:
                    _searchEntry.Visible = false;
                    
                    _startAtDateDaySb.Visible = false;
                    _startAtDateMonthSb.Visible = false;
                    _startAtDateYearSb.Visible = false;
                    
                    _startAtDaySb.Visible = false;
                    
                    _startAtMonthSb.Visible = false;
                    break;
            }
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

        private void DialogWindow(string message)
        {
            MessageDialog md = new MessageDialog(this,
                DialogFlags.DestroyWithParent, MessageType.Info,
                ButtonsType.Close, message);
            md.Run();
            md.Dispose();
        }
    }
}