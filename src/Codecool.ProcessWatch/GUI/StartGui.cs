using System;
using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class StartGui
    {
        public Window MainWindow;
        public StartGui()
        {
            Application.Init();
            MainWindow = new Gui();
            MainWindow.ShowAll();
            Application.Run();
        }
    }
}