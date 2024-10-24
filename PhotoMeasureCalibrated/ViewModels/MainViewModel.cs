
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PhotoMeasureCalibrated.Models;
using PhotoMeasureCalibrated.View;

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
    private double _mousePositionX;

    [ObservableProperty]
    private double _mousePositionY;


    [ObservableProperty]
    private bool _isEichungsDrawingEnabled;

    [ObservableProperty]
    private Point? _firstEichungsPoint;

    [ObservableProperty]
    private Point? _secondEichungsPoint;

    [ObservableProperty]
    private double _realeEichLaengeInCm;

    [ObservableProperty]
    private double _imageEichLaenge;

    // Collection to hold the shapes to draw on the canvas
    public ObservableCollection<UIElement> Shapes { get; set; } = new ObservableCollection<UIElement>();


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
    private void OnMouseMove(MouseEventArgs e)
    {
        if (e.Source is IInputElement element)
        {
            Point position = e.GetPosition(element);
            MousePositionX = position.X;
            MousePositionY = position.Y;
        }
    }


    [RelayCommand]
    public void ToggleEichungsDrawing()
    {
        IsEichungsDrawingEnabled = !IsEichungsDrawingEnabled; // Toggle drawing mode
        Mouse.OverrideCursor = IsEichungsDrawingEnabled ? Cursors.Cross : null;
    }

    [RelayCommand]
    private void MouseClick(MouseButtonEventArgs e)
    {
        if (!IsEichungsDrawingEnabled)
            return;

        // Get the clicked position on the image or canvas
        var position = e.GetPosition((IInputElement)e.Source);

        if (FirstEichungsPoint == null)
        {
            // First click: Save the point and draw a red dot
            FirstEichungsPoint = position;
            //DrawPoint(position);
            Shapes.Add(DrawingFigures.DrawPoint(position));
        }
        else if (SecondEichungsPoint == null)
        {
            // Second click: Save the second point, draw the second red dot and a line
            SecondEichungsPoint = position;
            Shapes.Add(DrawingFigures.DrawPoint(position));
            Shapes.Add(DrawingFigures.DrawLine(FirstEichungsPoint.Value, SecondEichungsPoint.Value));
        }

        if (FirstEichungsPoint != null && SecondEichungsPoint != null)
        {
            if (RealeEichLaengeInCm > 0)
            {
                var eichung = new Eichungsmodell(FirstEichungsPoint.Value, SecondEichungsPoint.Value, RealeEichLaengeInCm);
                ImageEichLaenge = eichung.PointDistance;
            }

            FirstEichungsPoint = null;// Reset points after drawing the line
            SecondEichungsPoint = null;
            ToggleEichungsDrawing();
        }
    }
}