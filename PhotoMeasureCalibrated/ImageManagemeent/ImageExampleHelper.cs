using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

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


        public static void LoadSampleImage(RadImageEditor imageEditor, string image)
        {
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + image)).Stream)
            {
                imageEditor.Image = new Telerik.Windows.Media.Imaging.RadBitmap(stream);
                imageEditor.ApplyTemplate();
                imageEditor.ScaleFactor = 0;
            }
        }

        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ImageExampleHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
