using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class HelpApp : Window
    {
        public HelpApp(Window parent) : base("Help")
        {
            WindowPosition = WindowPosition.CenterOnParent;
            TransientFor = parent;
            SetSizeRequest(480, 360);
            this.TypeHint = Gdk.WindowTypeHint.Dialog;
            this.Resizable = false;

            TextView helpTextView = new TextView();
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            helpTextView.Editable = false;
            helpTextView.Sensitive = false;
            helpTextView.Buffer.Text = HelpText();
            scrolledWindow.Add(helpTextView);
            
            Add(scrolledWindow);
            ShowAll();
        }

        private string HelpText()
        {
            return @"
                                                                      HELP
                                                                ""Process Watch""

    Buttons' top menu:
        - Run / Stop auto refresh view: start or stop automatic process view refresh.
            The view is refreshed every half second.

        - Refresh view: manual refreshing of the process view.

        - Help: information on how to operate the application.

        - About application: information about the application version, authors, 
            website and license.


    Available filters:
        - processes show all: all available processes on the computer.

        - processes name: filtering / searching processes by name.

        - processes started at date: filtering / searching for processes that 
            started on a given date.

        - processes started at day: filtering / searching for processes that 
            have started on a given day takes into account all processes 
            from each month.

        - processes started at month: filtering / searching for processes that 
            have started in a given month includes all processes from each year.

        - processes started before date: filtering / searching for processes that 
            started before the given date. The current hour and minutes are used 
            for filtering.

        - processes started after date: filtering / searching for processes that 
            started after the given date. The current hour and minutes are used 
            for filtering.

        - processes physical memory usage greater than: filtering / searching for 
            processes that physical memory usage is greater than given value.

        - processes user CPU time greater than: filtering / searching for processes 
            that user CPU time is greater than given time.

        - processes total CPU time greater than: filtering / searching for processes 
            that total CPU time is greater than given time.


    Processes view:
        Double-clicking on a selected process in the process view adds it to 
            the list of processes to be killed.


    Tools for killing processes:
        - The label with ""Number processes to kill"": shows the number of selected 
            processes to kill.

        - The button ""Show processes to kill"": shows the list of selected processes 
            to be stopped (process number and process name).

        - The button ""Clear processes to kill"": removes all selected processes 
            to be killed from the list.

        - The button ""Kill selected"": kills all processes from the list.


    Pagination buttons:
        The previous and next buttons switch to other pages on the list of processes.

    ";
        }
    }
}