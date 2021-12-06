using HitachiTask.Contracts;
using HitachiTask.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace HitachiTaskTests
{
    [TestFixture]
    public class CSVHelperTests
    {
        [Test]
        public void ExtractCsvFile()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var fileName = Path.Combine(projectPath, @"Resources\test.csv");
            var extractedList = CSVHelper.ExtractCsvFile(fileName);

            Assert.IsNotNull(extractedList);
            Assert.AreEqual(3, extractedList.Count);
            Assert.AreEqual("Dimitar", extractedList[0].FirstName);
            Assert.AreEqual("Davidkov", extractedList[0].LastName);
            Assert.AreEqual("Bulgaria", extractedList[0].Country);
            Assert.AreEqual("Sofia", extractedList[0].City);
            Assert.AreEqual(2451, extractedList[0].Score);
        }

        [Test]
        public void CreateCsvFile()
        {
            var csvOutputs = CreateSampleDtos();
            var createdFilePath = CSVHelper.CreateCsvFile(csvOutputs, "ReportByCountry.csv");

            Assert.IsNotNull(createdFilePath);
        }

        #region Helper methods
        private List<CsvOutputDto> CreateSampleDtos()
        {
            return new List<CsvOutputDto>
            {
                new CsvOutputDto()
                {
                    Country = "Bulgaria",
                    AverageScore = 1968,
                    MedianScore = 1225.5,
                    MaxScore = 2451,
                    MaxScorePerson = new ScorePerson() { FirstName = "Dimitar", LastName = "Davidkov" },
                    MinScore = 1598,
                    MinScorePerson = new ScorePerson() { FirstName = "Lilia", LastName = "Tsvetanova" },
                    RecordCount = 3
                },
                new CsvOutputDto()
                {
                    Country = "Great Britain",
                    AverageScore = 1707,
                    MedianScore = 1707,
                    MaxScore = 2259,
                    MaxScorePerson = new ScorePerson() { FirstName = "Adriana", LastName = "Tsankova" },
                    MinScore = 1155,
                    MinScorePerson = new ScorePerson() { FirstName = "Max", LastName = "Mustermann" },
                    RecordCount = 2
                }
            };
        }
        #endregion
    }
}