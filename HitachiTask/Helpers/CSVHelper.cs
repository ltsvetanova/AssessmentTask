using CsvHelper;
using CsvHelper.Configuration;
using HitachiTask.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HitachiTask.Helpers
{
    public static class CSVHelper
    {
        private static CsvConfiguration _csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        /// <summary>
        /// Reads the objects from the given .csv file
        /// </summary>
        public static List<CsvInputDto> ExtractCsvFile(string file)
        {
            List<CsvInputDto> inputCsv = new List<CsvInputDto>();
            try
            {
                using (var streamReader = File.OpenText(file))
                using (var csvReader = new CsvReader(streamReader, _csvConfig))
                    inputCsv = csvReader.GetRecords<CsvInputDto>().ToList();

            }
            catch(Exception ex)
            {
                throw new ExtractCsvFileException($"Exception occured while extracting the .csv file: {ex.Message}");
            }

            return inputCsv;
        }

        /// <summary>
        /// Creates the new .csv file
        /// </summary>
        public static string CreateCsvFile(List<CsvOutputDto> outputCsvs, string fileName)
        {
            try
            {
                string filePath = System.IO.Path.GetTempPath() + fileName;
                using (var streamWriter = new StreamWriter(filePath))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, _csvConfig))
                    {
                        csvWriter.WriteHeader<CsvOutputDto>();
                        csvWriter.NextRecord();
                        csvWriter.WriteRecords(outputCsvs);
                    }
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                throw new CreateReportException($"Exception occured while creating the new .csv file: {ex.Message}");
            }
        }

    }
    
}
