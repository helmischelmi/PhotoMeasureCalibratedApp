
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PhotoMeasureCalibrated.Models;

namespace PhotoMeasureCalibrated.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainModel Model { get; }

    [ObservableProperty]
    private string _imagePath;

    [ObservableProperty]
    private BitmapImage _bitmap;

    [ObservableProperty]
    private int _imagePixelWidth;

    [ObservableProperty]
    private int _imagePixelHeight;

    [ObservableProperty]
    private bool _isDrawingEnabled;

    public MainViewModel(MainModel model)
    {
        Model = model;
        ImagePixelHeight = 0;
        ImagePixelWidth = 0;
    }

    [RelayCommand]
    public async Task SelectFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select image",
            DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Favorites)
        };

        if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.FileName))
        {
            ImagePath = dialog.FileName;
        }

        using (FileStream stream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // Ensure the entire image is loaded into memory
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            bitmap.Freeze(); // Freeze to make it cross-thread safe and improve performance

            Bitmap = bitmap;
            ImagePixelWidth = Bitmap.PixelWidth;
            ImagePixelHeight = Bitmap.PixelHeight;
        }
    }

    [RelayCommand]
    public void EnableDrawing()
    {
        IsDrawingEnabled = !IsDrawingEnabled; // Toggle drawing mode

        // Change cursor to cross when drawing is enabled
        Mouse.OverrideCursor = IsDrawingEnabled ? Cursors.Cross : null;
    }
}