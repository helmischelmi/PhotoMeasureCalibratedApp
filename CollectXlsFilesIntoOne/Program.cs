using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace CollectXlsFilesIntoOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string startPfad = @"D:\01_Fotofalle\01_Fotofalledaten\BE102_Niederscherli\Feuersalamander_Zuordnung\BE102_Salamander_Identifikationliste"; // Startpfad anpassen
            List<AnimalLengthModel> gesammelteDaten = new();

            // Durchsuche das Verzeichnis nach Excel-Dateien
            foreach (string dateiPfad in Directory.GetFiles(startPfad, "*calibrated*.xlsx", SearchOption.AllDirectories))
            {
                // Öffne die Excel-Datei und lese die ersten zwei Zeilen und drei Spalten
                using (var workbook = new XLWorkbook(dateiPfad))
                {
                    var worksheet = workbook.Worksheet(1); // Annahme: erste Arbeitsblatt

                    // Lese die ersten zwei Zeilen und drei Spalten
                    for (int row = 2; row <= 2; row++)
                    {
                        var dataRow = new AnimalLengthModel();
                        for (int column = 1; column <= 4; column++)
                        {
                            switch (column)
                            {
                                case 1:
                                    dataRow.AddIndividuum(worksheet.Cell(row, column).Value.ToString());
                                    break;

                                case 2:
                                    dataRow.AddDatum(worksheet.Cell(row, column).Value.ToString());
                                    break;

                                case 3:
                                    dataRow.AddLength(worksheet.Cell(row, column).Value.ToString());
                                    break;

                                case 4:
                                    dataRow.AddLengthQuality(worksheet.Cell(row, column).Value.ToString());
                                    break;

                                default:
                                    throw new NotImplementedException();
                            }
                        }
                        gesammelteDaten.Add(dataRow);
                    }
                }

                Console.WriteLine($"Datei verarbeitet: {dateiPfad}");
            }

            // Speichere die gesammelten Daten in eine neue Excel-Datei
            string ausgabeDatei = Path.Combine(startPfad, "Tierlaengen.xlsx");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Längenmessung");

                //Schreibe die Daten in die neue Arbeitsmappe

                worksheet.Cell(1, 1).Value = "Individuum";
                worksheet.Cell(1, 2).Value = "Aufnahmedatum";
                worksheet.Cell(1, 3).Value = "LängeInCm";

                for (int i = 1; i < gesammelteDaten.Count; i++)
                {
                    worksheet.Cell(i + 1, 1).Value = gesammelteDaten[i].Individuum;
                    worksheet.Cell(i + 1, 2).Value = gesammelteDaten[i].Aufnahmedatum;
                    worksheet.Cell(i + 1, 3).Value = gesammelteDaten[i].Length;
                }

                workbook.SaveAs(ausgabeDatei);
            }

            Console.WriteLine($"Erstellung der Datei abgeschlossen: {ausgabeDatei}");
        }

    }
}
