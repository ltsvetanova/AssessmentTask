using HitachiTask.Contracts;
using HitachiTask.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace HitachiTaskTests
{
    [TestFixture]
    public class ReportHelperTests
    {
        [Test]
        public void CreateReportByCountry()
        {
            var csvInputs = CreateSampleDtos();
            var report = ReportHelper.CreateReportByCountry(csvInputs);
            Assert.IsNotNull(report);
            Assert.AreEqual(2, report.Count);

            // First record for the country "Bulgaria"
            Assert.AreEqual("Bulgaria", report[0].Country);
            Assert.AreEqual(1968, report[0].AverageScore);
            Assert.AreEqual(1225.5, report[0].MedianScore);
            Assert.AreEqual(csvInputs[1].Score, report[0].MaxScore);
            Assert.AreEqual(csvInputs[1].FirstName, report[0].MaxScorePerson.FirstName);
            Assert.AreEqual(csvInputs[1].LastName, report[0].MaxScorePerson.LastName);
            Assert.AreEqual(csvInputs[0].Score, report[0].MinScore);
            Assert.AreEqual(csvInputs[0].FirstName, report[0].MinScorePerson.FirstName);
            Assert.AreEqual(csvInputs[0].LastName, report[0].MinScorePerson.LastName);
            Assert.AreEqual(3, report[0].RecordCount);

            // Second record for the country "Great Britain"
            Assert.AreEqual("Great Britain", report[1].Country);
            Assert.AreEqual(1707, report[1].AverageScore);
            Assert.AreEqual(1707, report[1].MedianScore);
            Assert.AreEqual(csvInputs[3].Score, report[1].MaxScore);
            Assert.AreEqual(csvInputs[3].FirstName, report[1].MaxScorePerson.FirstName);
            Assert.AreEqual(csvInputs[3].LastName, report[1].MaxScorePerson.LastName);
            Assert.AreEqual(csvInputs[4].Score, report[1].MinScore);
            Assert.AreEqual(csvInputs[4].FirstName, report[1].MinScorePerson.FirstName);
            Assert.AreEqual(csvInputs[4].LastName, report[1].MinScorePerson.LastName);
            Assert.AreEqual(2, report[1].RecordCount);
        }

        #region Helper methods
        private List<CsvInputDto> CreateSampleDtos()
        {
            return new List<CsvInputDto>
            {
                new CsvInputDto()
                {
                    FirstName = "Lilia",
                    LastName =  "Tsvetanova",
                    Country = "Bulgaria",
                    City =  "Blagoevgrad",
                    Score = 1598
                },
                new CsvInputDto()
                {
                    FirstName = "Dimitar",
                    LastName =  "Davidkov",
                    Country = "Bulgaria",
                    City =  "Sofia",
                    Score = 2451
                },
                new CsvInputDto()
                {
                    FirstName = "Stela",
                    LastName =  "Valcheva",
                    Country = "Bulgaria",
                    City =  "Plovdiv",
                    Score = 1855
                },
                new CsvInputDto()
                {
                    FirstName = "Adriana",
                    LastName =  "Tsankova",
                    Country = "Great Britain",
                    City =  "London",
                    Score = 2259
                },
                new CsvInputDto()
                {
                    FirstName = "Max",
                    LastName =  "Mustermann",
                    Country = "Great Britain",
                    City =  "London",
                    Score = 1155
                }
            };
        }
        #endregion
    }
}