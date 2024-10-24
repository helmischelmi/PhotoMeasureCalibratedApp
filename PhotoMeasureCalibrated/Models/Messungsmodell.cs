
namespace PhotoMeasureCalibrated.Models;

public class Messungsmodell
{
    public List<ImagePoint> Messpunkte { get; set; }


    public double LaengenMessung
    {
        get
        {
            double laenge=0;

            for (int i = 0; i < Messpunkte.Count - 1; i++)
            {
                ImagePoint p1 = Messpunkte[i];
                ImagePoint p2 = Messpunkte[i + 1];

                laenge += GetPointDistance(Messpunkte[i], Messpunkte[i + 1]);
            }
            return Messpunkte.Count;
        }

    }

    public Messungsmodell()
    {
        Messpunkte = new List<ImagePoint>();
    }

    public void AddMesspunkt(int x, int y)
    {
        Messpunkte.Add(new ImagePoint(x, y));
    }


    private double GetPointDistance(ImagePoint? p1, ImagePoint? p2)
    {
        if (p1 == null || p2 == null) return 0;

        double deltaX = p2.X - p1.X;
        double deltaY = p2.Y - p1.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}