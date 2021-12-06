
namespace HitachiTask.Contracts
{
    public class CsvOutputDto
    {
        /// <summary>
        /// This is the unique row
        /// It is grouped by the country
        /// </summary>
        public string       Country         { get; set; }
        public double       AverageScore    { get; set; }
        public double       MedianScore     { get; set; }
        public int          MaxScore        { get; set; }
        public ScorePerson  MaxScorePerson  { get; set; }
        public int          MinScore        { get; set; }
        public ScorePerson  MinScorePerson  { get; set; }
        public int          RecordCount     { get; set; }
    }

    public class ScorePerson
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
    }
}
