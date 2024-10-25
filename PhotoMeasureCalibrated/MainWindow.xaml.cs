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
        
        // Handle mouse left button click on the image
        //private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Point mousePosition = e.GetPosition(DisplayedImage);

        //    if (((MainViewModel)DataContext).IsDrawCalibrationEnabled)
        //    {
        //        DrawPoint(mousePosition.X, mousePosition.Y);
        //    }
        //}


        //// Method to draw a point at the given coordinates
        //private void DrawPoint(double x, double y)
        //{
        //    // Create a small circle (Ellipse) as the point symbol
        //    Ellipse point = new Ellipse
        //    {
        //        Width = 10,
        //        Height = 10,
        //        Fill = Brushes.Red,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 1
        //    };

        //    // Set the position of the circle (centered at the clicked point)
        //    Canvas.SetLeft(point, x - point.Width / 2);
        //    Canvas.SetTop(point, y - point.Height / 2);

        //    // Add the circle to the Canvas overlay
        //    DrawingCanvas.Children.Add(point);
        //}


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