using System.Windows;

namespace PhotoMeasureCalibrated.Models;

public class CalibrationModel
{
    public Point? StartPoint { get; set; }

    public Point? EndPoint { get; set; }

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
        StartPoint =  new Point(x, y);
    }

    public void AddEndVertex(double x, double y)
    {
        EndPoint = new Point(x, y);
    }

    public double DistanceInImage
    {
        get
        {
            if (StartPoint == null || EndPoint == null) return 0;

            double deltaX = EndPoint.Value.X - StartPoint.Value.X;
            double deltaY = EndPoint.Value.Y - StartPoint.Value.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}