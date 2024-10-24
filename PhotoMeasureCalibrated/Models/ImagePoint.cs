namespace PhotoMeasureCalibrated.Models
{
    public class ImagePoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ImagePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
