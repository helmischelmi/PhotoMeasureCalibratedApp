using System.Windows;

namespace PhotoMeasureCalibrated.Models;

public class CalibrationModel
{
    public ImagePoint StartPoint { get; set; }

    public ImagePoint EndPoint { get; set; }

    public double RealDistanceInCm { get; set; }

    public CalibrationModel(Point start, Point end, double realDistanceInCm)
    {
        StartPoint = new ImagePoint(start.X, start.Y);
        EndPoint = new ImagePoint(end.X, end.Y);
        RealDistanceInCm = realDistanceInCm;
    }


    public double DistanceInImage
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