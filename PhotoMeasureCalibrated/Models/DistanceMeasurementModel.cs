
using System.Windows;
using System.Windows.Shapes;
using PhotoMeasureCalibrated.View;

namespace PhotoMeasureCalibrated.Models;


public class DistanceMeasurementModel
{
    public List<Point> LineVertices { get; set; }

    public List<Line> Lines { get; set; }


    public double MeasuredDistance
    {
        get
        {
            double laenge = 0;

            for (int i = 0; i < LineVertices.Count - 1; i++)
            {
                Point p1 = LineVertices[i];
                Point p2 = LineVertices[i + 1];

                laenge += GetPointDistance(LineVertices[i], LineVertices[i + 1]);
            }
            return laenge;
        }

    }

    public double RealMeasuredDistanceInCm { get; set; }

    public int MeasurementQuality { get; set; }

    public DistanceMeasurementModel()
    {
        LineVertices = new List<Point>();
        Lines = new List<Line>();
    }


    public void AddVertex(double x, double y)
    {
        LineVertices.Add(new Point(x, y));
    }

    public void AddQuality(int measurementQuality)
    {
        MeasurementQuality = measurementQuality;
    }

    private double GetPointDistance(Point? p1, Point? p2)
    {
        if (p1 == null || p2 == null) return 0;

        double deltaX = p2.Value.X - p1.Value.X;
        double deltaY = p2.Value.Y - p1.Value.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public Polyline EndMeasurement()
    {
        return DrawingFigures.DrawSplineOnCanvas(LineVertices);

    }

    public double GetRealDistanceInCm(CalibrationModel calibration)
    {
        RealMeasuredDistanceInCm = MeasuredDistance / (calibration.DistanceInImage / calibration.RealDistanceInCm);
        return RealMeasuredDistanceInCm;
    }
}