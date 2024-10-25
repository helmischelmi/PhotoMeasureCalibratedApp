
using System.Windows;
using System.Windows.Shapes;
using PhotoMeasureCalibrated.View;

namespace PhotoMeasureCalibrated.Models;


public class DistanceMeasurementModel
{
    public List<ImagePoint> LineVertices { get; set; }

    public List<Line> Lines { get; set; }


    public double MeasuredDistance
    {
        get
        {
            double laenge = 0;

            for (int i = 0; i < LineVertices.Count - 1; i++)
            {
                ImagePoint p1 = LineVertices[i];
                ImagePoint p2 = LineVertices[i + 1];

                laenge += GetPointDistance(LineVertices[i], LineVertices[i + 1]);
            }
            return laenge;
        }

    }

    public double RealMeasuredDistanceInCm { get; set; }



    public DistanceMeasurementModel()
    {
        LineVertices = new List<ImagePoint>();
        Lines = new List<Line>();
    }


    public void AddVertex(double x, double y)
    {
        LineVertices.Add(new ImagePoint(x, y));
    }


    private double GetPointDistance(ImagePoint? p1, ImagePoint? p2)
    {
        if (p1 == null || p2 == null) return 0;

        double deltaX = p2.X - p1.X;
        double deltaY = p2.Y - p1.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public List<Line> EndMeasurement()
    {


        for (int i = 0; i < LineVertices.Count - 1; i++)
        {
            Lines.Add(DrawingFigures.DrawMeasurementLine(new Point(LineVertices[i].X, LineVertices[i].Y),
                new Point(LineVertices[i + 1].X, LineVertices[i + 1].Y)));
        }


        return Lines;

        //
        // display distance in cm

    }

    public double GetRealDistanceInCm(CalibrationModel calibration)
    {
        RealMeasuredDistanceInCm = MeasuredDistance / (calibration.DistanceInImage / calibration.RealDistanceInCm);
        return RealMeasuredDistanceInCm;
    }
}