using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        }
        
        // Handle mouse left button click on the image
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(DisplayedImage);

            MousePositionX.Content = mousePosition.X;
            MousePositionY.Content = mousePosition.Y;

            if (((MainViewModel)DataContext).IsDrawingEnabled)
            {
                // Get the mouse position relative to the Image control


                // Get the image's actual width and height
                double imageActualWidth = DisplayedImage.ActualWidth;
                double imageActualHeight = DisplayedImage.ActualHeight;


                // Calculate the relative position (scale mouse coordinates to image coordinates)
                //double xRelative = mousePosition.X / imageActualWidth * ((MainViewModel)DataContext).ImagePixelWidth;
                //double yRelative = mousePosition.Y / imageActualHeight * ((MainViewModel)DataContext).ImagePixelHeight;

                double xRelative = mousePosition.X ;
                double yRelative = mousePosition.Y ;

                // Draw a point symbol at the calculated position
                DrawPoint(xRelative, yRelative);
            }
        }


        // Method to draw a point at the given coordinates
        private void DrawPoint(double x, double y)
        {
            // Create a small circle (Ellipse) as the point symbol
            Ellipse point = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            // Set the position of the circle (centered at the clicked point)
            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);

            // Add the circle to the Canvas overlay
            DrawingCanvas.Children.Add(point);
        }

        private void DisplayedImage_OnMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(DisplayedImage);

            MousePositionX.Content = mousePosition.X;
            MousePositionY.Content = mousePosition.Y;

            PointsWidth.Content= $"{DisplayedImage.ActualWidth:F0}" ;
            PointsHeight.Content= $"{DisplayedImage.ActualHeight:F0}" ;
        }
    }


}