﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using ExcelTestServices.Interfaaces;

namespace ExcelTestServices.Services
{
    public class FileFormatter : IFileFormatter
    {
        public async Task<bool> FromXlsxToCsv(string file, int lastYearNeeded)
        {
            var yearIterator = DateTime.Now.Year;
            var result = false;
            try
            {
                using (var workbook = new XLWorkbook(file))
                {
                    var newFile = file.Replace("xlsx", "csv");
                    var worksheet = workbook.Worksheet(1);

                    using (StreamWriter writer = new StreamWriter(newFile, false))
                    {
                        foreach (var row in worksheet.RowsUsed())
                        {
                            var cellsRow = row.Cells().Select(cell => cell.Value.ToString());
                            if (int.TryParse(cellsRow.First(), out var date) && date == lastYearNeeded) break;
                            writer.WriteLine(string.Join(",", cellsRow));
                        }
                    }
                }
                result = true;
                Console.WriteLine($"File transformed to csv.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in file transformation: {ex.Message}");
            }

            return result;
        }
    }
}
