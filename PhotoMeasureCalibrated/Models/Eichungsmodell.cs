using System.Windows;

namespace PhotoMeasureCalibrated.Models;

public class Eichungsmodell
{
    public ImagePoint StartPoint { get; set; }

    public ImagePoint EndPoint { get; set; }

    public double RealeLaengeInCm { get; set; }

    public Eichungsmodell(Point start, Point end, double realeLaengeInCm)
    {
        StartPoint = new ImagePoint(start.X, start.Y);
        EndPoint = new ImagePoint(end.X, end.Y);
        RealeLaengeInCm = realeLaengeInCm;
    }


    public double PointDistance
    {
        get
        {
            if (StartPoint == null || EndPoint == null) return 0;

            double deltaX = EndPoint.X - StartPoint.X;
            double deltaY = EndPoint.Y - StartPoint.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}