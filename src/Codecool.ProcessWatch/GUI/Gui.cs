using System;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class Gui : Window
    {
        public Gui() : base("Process Watch")
        {
            SetPosition(WindowPosition.Center);
            SetSizeRequest(650, 500);
            this.TypeHint = Gdk.WindowTypeHint.Normal;
            this.Resizable = true;
            DeleteEvent += delegate { Application.Quit(); };

            Button btn = new Button("TEST");
            
            Add(btn);
            ShowAll();
        }
    }
}