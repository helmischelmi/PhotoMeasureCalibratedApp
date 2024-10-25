
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PhotoMeasureCalibrated.Models;
using PhotoMeasureCalibrated.View;
using Path = System.IO.Path;

namespace PhotoMeasureCalibrated.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainModel Model { get; }

    [ObservableProperty]
    private string _creator;

    [ObservableProperty] 
    private string _individuumNummer;

    [ObservableProperty]
    private DateTime _imageTimestamp;

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
    private bool _isDrawCalibrationEnabled;

    [ObservableProperty]
    private bool _isDrawBodyLengthEnabled;

    //[ObservableProperty]
    //private Point? _calibrationStartpoint;

    //[ObservableProperty]
    //private Point? _calibrationEndpoint;

    [ObservableProperty]
    private double _realDistanceInCm;

    [ObservableProperty]
    private double _realBodyLength;

    [ObservableProperty]
    private double _imageCalibrationDistance;

    // Collection to hold the shapes to draw on the canvas
    public ObservableCollection<UIElement> Shapes { get; set; } = new();


    private DistanceMeasurementModel _measurementModel;

    private CalibrationModel _calibrationModel;

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

        Model.Path = Path.GetDirectoryName(ImagePath);
        Model.Filename = Path.GetFileName(ImagePath);
        IndividuumNummer = Model.IndividuumNumber;

        try
        {
            BitmapFrame frame = BitmapFrame.Create(new Uri(ImagePath), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            BitmapMetadata metadata = (BitmapMetadata)frame.Metadata;

            if (metadata != null && metadata.DateTaken != null)
            {
                Model.ImageTimestamp = Convert.ToDateTime(metadata.DateTaken);
                ImageTimestamp = Model.ImageTimestamp;
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception based on your app's requirements
            Console.WriteLine($"Error retrieving Date Taken metadata: {ex.Message}");
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
    public void ToggleCalibrationDrawing()
    {
        IsDrawCalibrationEnabled = !IsDrawCalibrationEnabled; // Toggle drawing mode
        Mouse.OverrideCursor = IsDrawCalibrationEnabled ? Cursors.Cross : null;

        if (IsDrawCalibrationEnabled)
        {
            _calibrationModel = new CalibrationModel();
        }
    }

    [RelayCommand]
    public void ToggleBodyDrawing()
    {
        IsDrawBodyLengthEnabled = !IsDrawBodyLengthEnabled; // Toggle drawing mode

        if (IsDrawBodyLengthEnabled)
        {
            _measurementModel = new DistanceMeasurementModel();
        }
        else
        {
            var lines = _measurementModel.EndMeasurement();

            RealBodyLength = _measurementModel.GetRealDistanceInCm(_calibrationModel);

            foreach (var line in lines)
            {
                Shapes.Add(line);
            }
        }
        Mouse.OverrideCursor = IsDrawBodyLengthEnabled ? Cursors.Pen : null;
    }

    [RelayCommand]
    private void SaveResults()
    {
        Model.Measurements = _measurementModel;
        Model.Creation = DateTime.UtcNow;
        Model.Calibration = _calibrationModel;
        Model.Creator = Creator;

        //public string Creator { get; set; }


        //public string Remarks { get; set; }

    }

    [RelayCommand]
    private void Reset()
    {
        Model.Reset();
    }


    [RelayCommand]
    private void MouseClick(MouseButtonEventArgs e)
    {
        bool isDrawingEnabled = IsDrawBodyLengthEnabled || IsDrawCalibrationEnabled;

        if (isDrawingEnabled == false) return;

        if (IsDrawCalibrationEnabled)
        {
            HandleCalibrationMouseClicks(e);
        }

        if (IsDrawBodyLengthEnabled)
        {
            HandleBodyLengthMeasureMouseClicks(e);
        }
    }


    private void HandleBodyLengthMeasureMouseClicks(MouseButtonEventArgs e)
    {
        // Get the clicked position on the image
        var position = e.GetPosition((IInputElement)e.Source);
        Shapes.Add(DrawingFigures.DrawMeasurementPoint(position));

        _measurementModel.AddVertex(position.X, position.Y);
    }


    private void HandleCalibrationMouseClicks(MouseButtonEventArgs e)
    {
        // Get the clicked position on the image
        var position = e.GetPosition((IInputElement)e.Source);
        
        if (_calibrationModel.StartPoint == null)
        {
            _calibrationModel.AddStartVertex(position.X, position.Y);

           // CalibrationStartpoint = position;// First click: Save the point and draw a red dot
            Shapes.Add(DrawingFigures.DrawCalibrationPoint(position));
        }
        else if (_calibrationModel.EndPoint == null)
        {
            // Second click: Save the second point, draw the second red dot and a line
            _calibrationModel.AddEndVertex(position.X, position.Y);
            Shapes.Add(DrawingFigures.DrawCalibrationPoint(position));
            Shapes.Add(DrawingFigures.DrawCalibrationLine(new Point(_calibrationModel.StartPoint.X,_calibrationModel.StartPoint.Y) 
                , new Point(_calibrationModel.EndPoint.X, _calibrationModel.EndPoint.Y)));
        }

        if (_calibrationModel.IsVertexCompleted)
        {
            if (RealDistanceInCm > 0)
            {
                _calibrationModel.RealDistanceInCm = RealDistanceInCm;

                ImageCalibrationDistance = _calibrationModel.DistanceInImage;
            }

            ToggleCalibrationDrawing();
        }
    }
}