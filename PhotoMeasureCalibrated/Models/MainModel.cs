namespace PhotoMeasureCalibrated.Models;

public class MainModel
{
    public string Path { get; set; }

    public string Filename { get; set; }

    public string IndividuumNumber
    {
        get
        {
            int index = Filename.IndexOf('_');

            return index >= 0 ? Filename.Substring(0, index) : Filename;
        }
    }

    public DateTime ImageTimestamp { get; set; }

    public DateTime Creation { get; set; }

    public string Creator { get; set; }

    public CalibrationModel Calibration { get; set; }

    public DistanceMeasurementModel Measurements { get; set; }

    public string Remarks { get; set; }

    public void Reset()
    {
        Path = null;
        Filename = null;
        ImageTimestamp = default;
        Creation = default;
        Calibration = new CalibrationModel();
        Measurements = new DistanceMeasurementModel();
        Remarks = null;
    }
}