using HitachiTask.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HitachiTask.Helpers
{
    public static class ReportHelper
    {
        /// <summary>
        /// Generates the report based on the given list of inputs
        /// </summary>
        public static List<CsvOutputDto> CreateReportByCountry(List<CsvInputDto> csvInputs)
        {
            try
            {
                List<CsvOutputDto> csvOutputs = new List<CsvOutputDto>();

                var groupedByCountry = csvInputs.GroupBy(x => x.Country);
                foreach (var groupByCountry in groupedByCountry)
                {
                    var groupedByList = groupByCountry.ToList();
                    var outputCsvDto = new CsvOutputDto
                    {
                        Country = groupByCountry.Key,
                        AverageScore = groupByCountry.Average(x => x.Score),
                        MedianScore = Median(groupedByList),
                        MaxScore = groupByCountry.Max(x => x.Score),
                        MinScore = groupByCountry.Min(x => x.Score),
                        RecordCount = groupByCountry.Count()
                    };

                    outputCsvDto.MaxScorePerson = GetScorePerson(groupedByList, outputCsvDto.MaxScore);
                    outputCsvDto.MinScorePerson = GetScorePerson(groupedByList, outputCsvDto.MinScore);
                    csvOutputs.Add(outputCsvDto);
                }

                csvOutputs.OrderByDescending(x => x.AverageScore).ToList();
                return csvOutputs;
            }
            catch(Exception ex)
            {
                throw new CreateReportException($"Exception occured while creating the new report: {ex.Message}");
            }
        }

        #region helper methods

        /// <summary>
        /// Computes the median based on the given list
        /// </summary>
        private static double Median(List<CsvInputDto> inputCsvDtos)
        {
            int numberCount = inputCsvDtos.Count();
            int halfIndex = numberCount / 2;
            double median;

            if (numberCount % 2 == 0)
                median = inputCsvDtos.ElementAt(halfIndex - 1).Score +
                           inputCsvDtos.ElementAt(halfIndex).Score;
            else
                median = inputCsvDtos.ElementAt(halfIndex).Score;

            return median / 2;
        }

        private static ScorePerson GetScorePerson(List<CsvInputDto> inputCsvDtos, int score)
        {
            return inputCsvDtos.Where(x => x.Score == score)
                               .Select(x => new ScorePerson
                               {
                                   FirstName = x.FirstName,
                                   LastName = x.LastName
                               })
                               .FirstOrDefault();
        }
        #endregion
    }
}
