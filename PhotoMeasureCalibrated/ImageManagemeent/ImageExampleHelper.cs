namespace PhotoMeasureCalibrated.ImageManagemeent
{
    public class ImageExampleHelper
    {
        public string ImagePath { get; }
        // Path.Combine(ImagePath,image)

        private static string SampleImageFolder = "SampleImages/";



        public ImageExampleHelper(string imagePath)
        {
            ImagePath = imagePath;
        }

       
    }
}
