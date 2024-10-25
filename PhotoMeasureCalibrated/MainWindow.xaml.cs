using System.Windows;
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
        }


        private void DisplayedImage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                PointsWidth.Content = $"{DisplayedImage.ActualWidth:F0}";
                PointsHeight.Content = $"{DisplayedImage.ActualHeight:F0}";

                //viewModel.ImageActualWidth = DisplayedImage.ActualWidth;
                //viewModel.ImageActualHeight = DisplayedImage.ActualHeight;
            }
        }
    }


}