using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PhotoMeasureCalibrated.View
{
    public class DrawingFigures
    {

        public static Ellipse DrawCalibrationPoint(Point point)
        {
            return DrawPoint(point, Brushes.Red,5);
        }

        public static Ellipse DrawMeasurementPoint(Point point)
        {
            return DrawPoint(point, Brushes.GreenYellow, 8);
        }

        public static Ellipse DrawPoint(Point point, Brush fill, double diameter)
        {
            // Create a red ellipse (dot) at the specified point
            Ellipse dot = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Fill = fill,
            };
            Canvas.SetLeft(dot, point.X - diameter/2); // Center the dot
            Canvas.SetTop(dot, point.Y - diameter/2);
            return dot;
        }

        public static Line DrawCalibrationLine(Point point1, Point point2)
        {
            return DrawLine(point1, point2, Brushes.Red, 2);
        }

        public static Line BeginDynamicLine(Point point1)
        {
            Line line = new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point1.X,
                Y2 = point1.Y,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            return line;
        }

        public static void UpdateDynamicLine(Line dynamicLine, Point point2)
        {
            dynamicLine.X2 = point2.X;
            dynamicLine.Y2 = point2.Y;
        }


        public static Line DrawMeasurementLine(Point point1, Point point2)
        {
            return DrawLine(point1, point2, Brushes.LightGreen, 4);
        }

        public static Line DrawLine(Point point1, Point point2, Brush stroke, double thickness)
        {
            // Create a line between the two points
            Line line = new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y,
                Stroke = stroke,
                StrokeThickness = thickness
            };

            return line;
        }

        public static Polyline DrawSplineOnCanvas(List<Point> points)
        {
            List<Point> splinePoints = SplineHelper.GenerateCatmullRomSpline(points);

            Polyline splinePolyline = new Polyline
            {
                Stroke = Brushes.LightGreen, // Set color
                StrokeThickness = 4,
                Points = new PointCollection(splinePoints)
            };

            return splinePolyline;
        }
    }
}
