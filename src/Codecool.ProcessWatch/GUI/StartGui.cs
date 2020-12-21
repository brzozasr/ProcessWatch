using Gtk;

namespace Codecool.ProcessWatch.GUI
{
    public class StartGui
    {
        public StartGui()
        {
            Application.Init();
            Gui mainWindow = new Gui();
            mainWindow.Show();
            Application.Run();
        }
    }
}