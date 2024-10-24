namespace PhotoMeasureCalibrated.Models;

public class Eichungsmodell
{
    public ImagePoint? StartPoint { get; set; }

    public ImagePoint? EndPoint { get; set; }

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

    public void AddStartPoint(int x, int y)
    {
        StartPoint = new ImagePoint(x, y);
    }

    public void AddEndPoint(int x, int y)
    {
        EndPoint = new ImagePoint(x, y);
    }
}