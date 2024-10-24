using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhotoMeasureCalibrated.View
{
    public class DrawingFigures
    {

        public static Ellipse DrawPoint(Point point)
        {
            // Create a red ellipse (dot) at the specified point
            Ellipse dot = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Red,
            };
            Canvas.SetLeft(dot, point.X - 2.5); // Center the dot
            Canvas.SetTop(dot, point.Y - 2.5);
            return dot;
        }
    }
}
