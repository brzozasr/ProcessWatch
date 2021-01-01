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
            helpTextView.Editable = false;
            helpTextView.Buffer.Text = HelpText();
            
            Add(helpTextView);
            ShowAll();
        }

        private string HelpText()
        {
            return @"
            Help for application ""Process Watch""

";
        }
    }
}