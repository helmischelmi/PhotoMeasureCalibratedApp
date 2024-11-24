
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PhotoMeasureCalibrated.Models;
using PhotoMeasureCalibrated.View;
using Path = System.IO.Path;
using Point = System.Windows.Point;

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

    [ObservableProperty]
    private bool _isImageLoaded;

    [ObservableProperty]
    private bool _isMeasureSaved;

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

    private Line _dynamicLine;

    public MainViewModel(MainModel model)
    {
        Model = model;
        ImagePixelHeight = 0;
        ImagePixelWidth = 0;

        RealDistanceInCm = SettingsWpf.Default.Eichungsdistanz;
        Creator = SettingsWpf.Default.Creator;
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

        if (File.Exists(ImagePath) == false)
        {
            return;
        }

        Model.Filepath = Path.GetDirectoryName(ImagePath);
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

        IsImageLoaded = true;
    }



    [RelayCommand]
    private void OnMouseMove(MouseEventArgs e)
    {
        if (e.Source is IInputElement element)
        {
            Point position = e.GetPosition(element);
            MousePositionX = position.X;
            MousePositionY = position.Y;

            if (_calibrationModel == null)
            {
                return;
            }
            // Aktualisiere die Linie dynamisch, wenn der Startpunkt gesetzt ist und kein Endpunkt existiert
            if (_calibrationModel.StartPoint.HasValue && _dynamicLine != null && _calibrationModel.EndPoint.HasValue == false)
            {
                DrawingFigures.UpdateDynamicLine(_dynamicLine, position);
            }
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
            var polyline = _measurementModel.EndMeasurement();
            Shapes.Add(polyline);

            RealBodyLength = _measurementModel.GetRealDistanceInCm(_calibrationModel);

        }
        Mouse.OverrideCursor = IsDrawBodyLengthEnabled ? Cursors.Pen : null;
    }

    [RelayCommand]
    private async Task SaveResults()
    {
        UpdateSettings();

        Model.Measurements = _measurementModel;
        Model.Creation = DateTime.UtcNow;
        Model.Calibration = _calibrationModel;
        Model.Creator = Creator;

        IsMeasureSaved = true;
        Model.SaveAsJson();
        Model.SaveToExcel();
        Task.Delay(2000);
    }


    [RelayCommand]
    private void Reset()
    {
        IsImageLoaded = false;
        IsMeasureSaved = false;

        Model.Reset();
        Model.Creation = default;

        IndividuumNummer = String.Empty;
        ImageTimestamp = default;
        ImagePath = String.Empty;
        Bitmap = null;
        ImagePixelWidth = 0;
        ImagePixelHeight = 0;
        RealBodyLength = 0;
        ImageCalibrationDistance = 0;
        Shapes.Clear();
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

            // Initialisiere die Linie
            _dynamicLine = DrawingFigures.BeginDynamicLine(position);

            // CalibrationStartpoint = position;// First click: Save the point and draw a red dot
            Shapes.Add(DrawingFigures.DrawCalibrationPoint(position));
            Shapes.Add(_dynamicLine);
        }
        else if (_calibrationModel.EndPoint == null)
        {
            // Second click: Save the second point, draw the second red dot and a line
            _calibrationModel.AddEndVertex(position.X, position.Y);

            _dynamicLine.X2 = position.X;
            _dynamicLine.Y2 = position.Y;
            Shapes.Remove(_dynamicLine);
            _dynamicLine = null; // Beende die dynamische Linie

            Shapes.Add(DrawingFigures.DrawCalibrationPoint(position));
            Shapes.Add(DrawingFigures.DrawCalibrationLine(
                new Point(_calibrationModel.StartPoint.Value.X, _calibrationModel.StartPoint.Value.Y),
                new Point(_calibrationModel.EndPoint.Value.X, _calibrationModel.EndPoint.Value.Y)));
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

    private void UpdateSettings()
    {
        SettingsWpf.Default.Eichungsdistanz = RealDistanceInCm;
        SettingsWpf.Default.Creator = Creator;
        SettingsWpf.Default.Save();
    }
}