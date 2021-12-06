using CsvHelper.Configuration.Attributes;

namespace HitachiTask.Contracts
{
    public class CsvInputDto
    {
        [Name("First Name")]
        public string   FirstName   { get; set; }
        
        [Name("Last Name")]
        public string   LastName    { get; set; }
        public string   Country     { get; set; }
        public string   City        { get; set; }
        public int      Score       { get; set; }
    }
}
