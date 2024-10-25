using System.Windows;

namespace PhotoMeasureCalibrated.Models;

public class CalibrationModel
{
    public ImagePoint StartPoint { get; set; }

    public ImagePoint EndPoint { get; set; }

    public double RealDistanceInCm { get; set; }


    public bool IsVertexCompleted
    {
        get
        {
            return StartPoint != null && EndPoint != null;
        }
    }


    public void GetRealDistanceInCm(double realDistance)
    {
        RealDistanceInCm = realDistance;
    }


    public void AddStartVertex(double x, double y)
    {
        StartPoint =  new ImagePoint(x, y);
    }

    public void AddEndVertex(double x, double y)
    {
        EndPoint = new ImagePoint(x, y);
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