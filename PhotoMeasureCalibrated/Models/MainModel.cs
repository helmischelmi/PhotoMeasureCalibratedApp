using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeOpenXml;

namespace PhotoMeasureCalibrated.Models;

public class MainModel
{
    public string Filepath { get; set; }

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

    //public string Remarks { get; set; }

    public void Reset()
    {
        Filepath = null;
        Filename = null;
        ImageTimestamp = default;
        Creation = default;
        Calibration = new CalibrationModel();
        Measurements = new DistanceMeasurementModel();
        //Remarks = null;
    }

    public void SaveAsJson()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Optional: formats the JSON with indentation for readability
                ReferenceHandler = ReferenceHandler.Preserve // Optional: handles circular references if necessary
            };

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(Path.Combine(Filepath, $"{Path.GetFileNameWithoutExtension(Filename)}_calibratedMeasure.json"), jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving MainModel data to JSON: {ex.Message}");
        }
    }

    public void SaveToExcel()
    {
        string filePath = Path.Combine(Filepath, $"{Path.GetFileNameWithoutExtension(Filename)}_calibratedMeasure.xlsx");

        ExcelPackage.License.SetNonCommercialOrganization("My person");

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Längenmessung");

            // Kopfzeilen schreiben
            worksheet.Cells[1, 1].Value = "Individuum";
            worksheet.Cells[1, 2].Value = "Aufnahmedatum";
            worksheet.Cells[1, 3].Value = "Länge in cm";
            worksheet.Cells[1, 4].Value = "Qualität";

            // Werte in die erste Datenzeile schreiben
            worksheet.Cells[2, 1].Value = IndividuumNumber;
            worksheet.Cells[2, 2].Value = ImageTimestamp;
            worksheet.Cells[2, 3].Value = Measurements.RealMeasuredDistanceInCm;
            worksheet.Cells[2, 4].Value = Measurements.MeasurementQuality;

            // Formatierung für das Datum
            worksheet.Cells[2, 2].Style.Numberformat.Format = "dd.MM.yyyy";

            // Datei speichern
            FileInfo fileInfo = new FileInfo(filePath);
            package.SaveAs(fileInfo);
        }
    }
}