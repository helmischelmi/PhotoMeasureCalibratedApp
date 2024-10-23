using PhotoMeasureCalibrated.ImageManagemeent;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PhotoMeasureCalibrated.ViewModels;

namespace PhotoMeasureCalibrated
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //  ImageExampleHelper.LoadSampleImage(this.ImageEditor, "SampleImage.png");
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ImageEditor.Commands.Rotate180.Execute(this.ImageEditor);
        }
    }
}