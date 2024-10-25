using System.Windows;

namespace PhotoMeasureCalibrated.View;

public static class SplineHelper
{
    public static List<Point> GenerateCatmullRomSpline(List<Point> points, double tension = 0.5, int segments = 20)
    {
        List<Point> splinePoints = new List<Point>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Point p0 = i > 0 ? points[i - 1] : points[i];
            Point p1 = points[i];
            Point p2 = points[i + 1];
            Point p3 = i < points.Count - 2 ? points[i + 2] : p2;

            for (int j = 0; j < segments; j++)
            {
                double t = j / (double)segments;
                Point splinePoint = CatmullRomInterpolate(p0, p1, p2, p3, t, tension);
                splinePoints.Add(splinePoint);
            }
        }

        splinePoints.Add(points[^1]); // Add the last point explicitly
        return splinePoints;
    }

    private static Point CatmullRomInterpolate(Point p0, Point p1, Point p2, Point p3, double t, double tension)
    {
        double t2 = t * t;
        double t3 = t2 * t;

        double x = 0.5 * ((2 * p1.X) +
                          (-p0.X + p2.X) * t +
                          (2 * p0.X - 5 * p1.X + 4 * p2.X - p3.X) * t2 +
                          (-p0.X + 3 * p1.X - 3 * p2.X + p3.X) * t3);

        double y = 0.5 * ((2 * p1.Y) +
                          (-p0.Y + p2.Y) * t +
                          (2 * p0.Y - 5 * p1.Y + 4 * p2.Y - p3.Y) * t2 +
                          (-p0.Y + 3 * p1.Y - 3 * p2.Y + p3.Y) * t3);

        return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
    }
}