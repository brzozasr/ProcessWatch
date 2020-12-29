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
        private string _filterType = "AllProcesses";
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
        private SpinButton _startBeforeDateDaySb;
        private SpinButton _startBeforeDateMonthSb;
        private SpinButton _startBeforeDateYearSb;
        private SpinButton _startAfterDateDaySb;
        private SpinButton _startAfterDateMonthSb;
        private SpinButton _startAfterDateYearSb;
        private SpinButton _memoryUsageSb;
        private Label _memoryUsageLbl;
        private SpinButton _userCpuTimeSb;
        private Label _userCpuTimeLbl;
        private SpinButton _totalCpuTimeSb;
        private Label _totalCpuTimeLbl;

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
            _searchEntry.Changed += OnChangeSearchEntry;

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
            _startAtDateDaySb.Changed += OnChangeAtDateDay;
            _startAtDateMonthSb.Changed += OnChangeAtDateMonth;
            _startAtDateYearSb.Changed += OnChangeAtDateYear;

            _startAtDaySb = new SpinButton(1, 31, 1);
            _startAtDaySb.Value = DateTime.Now.Day;
            _startAtDaySb.Visible = false;
            _topRightHBox.Add(_startAtDaySb);

            _startAtMonthSb = new SpinButton(1, 12, 1);
            _startAtMonthSb.Value = DateTime.Now.Month;
            _startAtMonthSb.Visible = false;
            _topRightHBox.Add(_startAtMonthSb);

            _startBeforeDateDaySb = new SpinButton(1, 31, 1);
            _startBeforeDateDaySb.Value = DateTime.Now.Day;
            _startBeforeDateDaySb.Visible = false;
            _startBeforeDateMonthSb = new SpinButton(1, 12, 1);
            _startBeforeDateMonthSb.Value = DateTime.Now.Month;
            _startBeforeDateMonthSb.Visible = false;
            _startBeforeDateYearSb = new SpinButton(1990, 2200, 1);
            _startBeforeDateYearSb.Value = DateTime.Now.Year;
            _startBeforeDateYearSb.Visible = false;
            _topRightHBox.Add(_startBeforeDateDaySb);
            _topRightHBox.Add(_startBeforeDateMonthSb);
            _topRightHBox.Add(_startBeforeDateYearSb);

            _startAfterDateDaySb = new SpinButton(1, 31, 1);
            _startAfterDateDaySb.Value = DateTime.Now.Day;
            _startAfterDateDaySb.Visible = false;
            _startAfterDateMonthSb = new SpinButton(1, 12, 1);
            _startAfterDateMonthSb.Value = DateTime.Now.Month;
            _startAfterDateMonthSb.Visible = false;
            _startAfterDateYearSb = new SpinButton(1990, 2200, 1);
            _startAfterDateYearSb.Value = DateTime.Now.Year;
            _startAfterDateYearSb.Visible = false;
            _topRightHBox.Add(_startAfterDateDaySb);
            _topRightHBox.Add(_startAfterDateMonthSb);
            _topRightHBox.Add(_startAfterDateYearSb);

            Adjustment memory = new Adjustment(1.0, 0.0001, 99999.9999, 0.01, 1, 1);
            _memoryUsageSb = new SpinButton(memory, 0.0, 4);
            _memoryUsageSb.Numeric = true;
            _memoryUsageSb.Visible = false;
            _memoryUsageLbl = new Label("  1 MB");
            _memoryUsageLbl.Visible = false;
            _topRightHBox.Add(_memoryUsageSb);
            _topRightHBox.Add(_memoryUsageLbl);
            _memoryUsageSb.Changed += OnChangeMemoryUsage;

            Adjustment userCpu = new Adjustment(1.0, 0.001, 99999.999, 0.01, 1, 1);
            _userCpuTimeSb = new SpinButton(userCpu, 0.0, 3);
            _userCpuTimeSb.Numeric = true;
            _userCpuTimeSb.Visible = false;
            _userCpuTimeLbl = new Label("  1 s");
            _userCpuTimeLbl.Visible = false;
            _topRightHBox.Add(_userCpuTimeSb);
            _topRightHBox.Add(_userCpuTimeLbl);
            _userCpuTimeSb.Changed += OnChangeUserCpuTime;
            
            Adjustment totalCpu = new Adjustment(1.0, 0.001, 99999.999, 0.01, 1, 1);
            _totalCpuTimeSb = new SpinButton(totalCpu, 0.0, 3);
            _totalCpuTimeSb.Numeric = true;
            _totalCpuTimeSb.Visible = false;
            _totalCpuTimeLbl = new Label("  1 s");
            _totalCpuTimeLbl.Visible = false;
            _topRightHBox.Add(_totalCpuTimeSb);
            _topRightHBox.Add(_totalCpuTimeLbl);
            _totalCpuTimeSb.Changed += OnChangeTotalCpuTime;
        }
        

        private void HideAllWidgets()
        {
            _searchEntry.Visible = false;

            _startAtDateDaySb.Visible = false;
            _startAtDateMonthSb.Visible = false;
            _startAtDateYearSb.Visible = false;

            _startAtDaySb.Visible = false;

            _startAtMonthSb.Visible = false;

            _startBeforeDateDaySb.Visible = false;
            _startBeforeDateMonthSb.Visible = false;
            _startBeforeDateYearSb.Visible = false;

            _startAfterDateDaySb.Visible = false;
            _startAfterDateMonthSb.Visible = false;
            _startAfterDateYearSb.Visible = false;

            _memoryUsageSb.Visible = false;
            _memoryUsageLbl.Visible = false;
            
            _userCpuTimeSb.Visible = false;
            _userCpuTimeLbl.Visible = false;
            
            _totalCpuTimeSb.Visible = false;
            _totalCpuTimeLbl.Visible = false;
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
                    _filterType = "AllProcesses";
                    HideAllWidgets();
                    _pageNo = 1;
                    InitAllProcesses();
                    break;
                case "- processes name":
                    _filterType = "ProcessesByName";
                    HideAllWidgets();
                    _searchEntry.Visible = true;
                    _pageNo = 1;
                    InitProcessesByName();
                    break;
                case "- processes started at date":
                    _filterType = "ProcessesStartAtDate";
                    HideAllWidgets();
                    _startAtDateDaySb.Visible = true;
                    _startAtDateMonthSb.Visible = true;
                    _startAtDateYearSb.Visible = true;
                    _pageNo = 1;
                    InitProcessesAtDate();
                    break;
                case "- processes started at day":
                    HideAllWidgets();
                    _startAtDaySb.Visible = true;
                    break;
                case "- processes started at month":
                    HideAllWidgets();
                    _startAtMonthSb.Visible = true;
                    break;
                case "- processes started before date":
                    HideAllWidgets();
                    _startBeforeDateDaySb.Visible = true;
                    _startBeforeDateMonthSb.Visible = true;
                    _startBeforeDateYearSb.Visible = true;
                    break;
                case "- processes started after date":
                    HideAllWidgets();
                    _startAfterDateDaySb.Visible = true;
                    _startAfterDateMonthSb.Visible = true;
                    _startAfterDateYearSb.Visible = true;
                    break;
                case "- processes physical memory usage greater than...":
                    HideAllWidgets();
                    _memoryUsageSb.Visible = true;
                    _memoryUsageLbl.Visible = true;
                    break;
                case "- processes user CPU time greater than...":
                    HideAllWidgets();
                    _userCpuTimeSb.Visible = true;
                    _userCpuTimeLbl.Visible = true;
                    break;
                case "- processes total CPU time greater than...":
                    HideAllWidgets();
                    _totalCpuTimeSb.Visible = true;
                    _totalCpuTimeLbl.Visible = true;
                    break;
                default:
                    HideAllWidgets();
                    break;
            }
        }

        private void OnChangeCommonMethod()
        {
            RemoveAllDataView();
            _pageNo = 1;
            RefreshView();
            if (_numberOfPages == 0)
            {
                _pageNoLbl.Text = $"0 of {_numberOfPages}";
            }
            else
            {
                _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            }

            SetNaviBtnSensitive();
        }

        private void OnChangeSearchEntry(object sender, EventArgs e)
        {
            OnChangeCommonMethod();
        }

        private void OnChangeMemoryUsage(object sender, EventArgs e)
        {
            string txt = "";
            double memory = _memoryUsageSb.Value;

            if (memory > 1024 * 1024)
            {
                memory = memory / 1024 / 1024;
                txt = $"  {Math.Round(memory, 2)} TB";
            }
            else if (memory > 1024)
            {
                memory = memory / 1024;
                txt = $"  {Math.Round(memory, 2)} GB";
            }
            else if (memory >= 1 && memory <= 1024)
            {
                txt = $"  {Math.Round(memory, 2)} MB";
            }
            else if (memory < 1)
            {
                memory = memory * 1024;
                txt = $"  {Math.Round(memory, 2)} kB";
            }

            _memoryUsageLbl.Text = txt;
        }
        
        private void OnChangeUserCpuTime(object sender, EventArgs e)
        {
            string txt = "";
            double time = _userCpuTimeSb.Value;

            if (time >= 1)
            {
                txt = $"  {Math.Round(time, 2)} s";
            }
            else if (time < 1)
            {
                time = time * 1000;
                txt = $"  {Math.Round(time, 2)} ms";
            }

            _userCpuTimeLbl.Text = txt;
        }
        
        private void OnChangeTotalCpuTime(object sender, EventArgs e)
        {
            string txt = "";
            double time = _totalCpuTimeSb.Value;

            if (time >= 1)
            {
                txt = $"  {Math.Round(time, 2)} s";
            }
            else if (time < 1)
            {
                time = time * 1000;
                txt = $"  {Math.Round(time, 2)} ms";
            }

            _totalCpuTimeLbl.Text = txt;
        }

        private void OnChangeAtDateYear(object sender, EventArgs e)
        {
            OnChangeCommonMethod();
        }

        private void OnChangeAtDateMonth(object sender, EventArgs e)
        {
            OnChangeCommonMethod();
        }

        private void OnChangeAtDateDay(object sender, EventArgs e)
        {
            OnChangeCommonMethod();
        }

        private void InitAllProcesses()
        {
            RemoveAllDataView();
            var processesData = ProcessWatchApplication.AllProcesses(PageSize, _pageNo);
            _processesList = processesData.ProcessesList;
            _numberOfPages = processesData.NumberOfPages;
            if (_numberOfPages == 0)
            {
                _pageNoLbl.Text = $"0 of {_numberOfPages}";
            }
            else
            {
                _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            }

            _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            RefreshView();
            SetNaviBtnSensitive();
        }

        private void InitProcessesByName()
        {
            RemoveAllDataView();
            var processesData = ProcessWatchApplication.SelectProcessesByName(PageSize, _pageNo, _searchEntry.Text);
            _processesList = processesData.ProcessesList;
            _numberOfPages = processesData.NumberOfPages;
            if (_numberOfPages == 0)
            {
                _pageNoLbl.Text = $"0 of {_numberOfPages}";
            }
            else
            {
                _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            }

            _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            RefreshView();
            SetNaviBtnSensitive();
        }

        private void InitProcessesAtDate()
        {
            RemoveAllDataView();
            var processesStartAtDate = ProcessWatchApplication.SelectProcessesStartAtDate(PageSize, _pageNo,
                (int) _startAtDateDaySb.Value, (int) _startAtDateMonthSb.Value,
                (int) _startAtDateYearSb.Value);
            _processesList = processesStartAtDate.ProcessesList;
            _numberOfPages = processesStartAtDate.NumberOfPages;
            if (_numberOfPages == 0)
            {
                _pageNoLbl.Text = $"0 of {_numberOfPages}";
            }
            else
            {
                _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            }

            _pageNoLbl.Text = $"{_pageNo} of {_numberOfPages}";
            RefreshView();
            SetNaviBtnSensitive();
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
                _nextBtn.Sensitive = true;
            }
            else if (_pageNo == _numberOfPages)
            {
                _previousBtn.Sensitive = true;
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
            // _processesList = ProcessWatchApplication.AllProcesses(PageSize, _pageNo).ProcessesList;
            GetProcessesList();

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

        private void GetProcessesList()
        {
            switch (_filterType)
            {
                case "AllProcesses":
                    var processesAll = ProcessWatchApplication.AllProcesses(PageSize, _pageNo);
                    _processesList = processesAll.ProcessesList;
                    _numberOfPages = processesAll.NumberOfPages;
                    break;
                case "ProcessesByName":
                    var processesName =
                        ProcessWatchApplication.SelectProcessesByName(PageSize, _pageNo, _searchEntry.Text);
                    _processesList = processesName.ProcessesList;
                    _numberOfPages = processesName.NumberOfPages;
                    break;
                case "ProcessesStartAtDate":
                    var processesStartAtDate = ProcessWatchApplication.SelectProcessesStartAtDate(PageSize, _pageNo,
                        (int) _startAtDateDaySb.Value, (int) _startAtDateMonthSb.Value,
                        (int) _startAtDateYearSb.Value);
                    _processesList = processesStartAtDate.ProcessesList;
                    _numberOfPages = processesStartAtDate.NumberOfPages;
                    break;
                default:
                    var processesDefault = ProcessWatchApplication.AllProcesses(PageSize, _pageNo);
                    _processesList = processesDefault.ProcessesList;
                    _numberOfPages = processesDefault.NumberOfPages;
                    break;
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