using System.Windows;

namespace eimg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                MainWindow mw = new MainWindow(e.Args[0]);
                mw.Show();
            }
            else
            {
                MainWindow mw = new MainWindow();
                mw.Show();
            }
        }
    }
}