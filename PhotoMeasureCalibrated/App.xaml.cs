using System.Windows;
using PhotoMeasureCalibrated.Models;
using PhotoMeasureCalibrated.ViewModels;

namespace PhotoMeasureCalibrated
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new MainModel();
            var mainViewModel = new MainViewModel(model);
            var mainWindow = new MainWindow {DataContext = mainViewModel};
            
            mainWindow.Show();
        }
    }

}
