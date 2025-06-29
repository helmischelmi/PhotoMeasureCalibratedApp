using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace JsonAndXlsxUpdateHelper;

internal class Program
{
    /// <summary>
    ///  finds all json-files in the given directory and its subdirectories,
    ///  Inserts a new line with "MeasurementQuality": 4 after lines containing "RealMeasuredDistanceInCm" followed by a closing brace "}",
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        string startDirectory = @"D:\01_Fotofalle\01_Fotofalledaten\BE102_Niederscherli\Feuersalamander_Zuordnung\BE102_Salamander_Identifikationliste";

        var jsonFiles = new List<string>(
            Directory.GetFiles(startDirectory, "*.json", SearchOption.AllDirectories)
        );

        Console.WriteLine($"Gefundene JSON-Dateien: {jsonFiles.Count}");

        foreach (var file in jsonFiles)
        {
            var lines = new List<string>(File.ReadAllLines(file));
            bool modified = false;

            for (int i = 0; i < lines.Count - 1; i++)
            {
                string currentLine = lines[i].Trim();
                string nextLine = lines[i + 1].Trim();

                if (currentLine.StartsWith("\"RealMeasuredDistanceInCm\"") &&
                    nextLine == "}")
                {
                    lines[i] = lines[i] + ",";
                    lines.Insert(i + 1, "    \"MeasurementQuality\": 4");
                    modified = true;
                    i++; 
                    Console.WriteLine($"Match und Änderung in Datei: {file}");
                }
            }

            if (modified)
            {
                File.WriteAllLines(file, lines);
            }
        }

        Console.WriteLine("Fertig mit JSON!");

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        // Nur Dateien, deren Name mit calibratedMeasure endet (ohne Extension)
        var xlsxFiles = new List<string>();
        foreach (var path in Directory.GetFiles(startDirectory, "*.xlsx", SearchOption.AllDirectories))
        {
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            if (fileNameWithoutExt.EndsWith("calibratedMeasure", StringComparison.OrdinalIgnoreCase))
            {
                xlsxFiles.Add(path);
            }
        }

        Console.WriteLine($"Gefundene Excel-Dateien: {xlsxFiles.Count}");

        foreach (var file in xlsxFiles)
        {
            bool modified = false;

            using (var package = new ExcelPackage(new FileInfo(file)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Erstes Arbeitsblatt

                // Zellen sind 1-basiert: D1 = [1,4], D2 = [2,4]
                var headerValue = worksheet.Cells[1, 4].Text;

                if (string.IsNullOrWhiteSpace(headerValue) || headerValue != "Qualität")
                {
                    worksheet.Cells[1, 4].Value = "Qualität";
                    worksheet.Cells[2, 4].Value = 4;
                    modified = true;
                    Console.WriteLine($"Qualität ergänzt in Datei: {file}");
                }
                // Falls verändert, speichern
                if (modified)
                {
                    // Originaldatei überschreiben
                    File.Delete(file); // Muss gelöscht werden, sonst gibt es IO-Fehler
                    package.SaveAs(new FileInfo(file));
                }
            }
        }
        Console.WriteLine("Excel-Update abgeschlossen!");

    }
}