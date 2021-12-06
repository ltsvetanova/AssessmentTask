using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiTask.Helpers
{
    public class ExtractCsvFileException : Exception
    {
        public ExtractCsvFileException(string message)
            : base(message) { }
    }

    public class CreateCSVFileException : Exception
    {
        public CreateCSVFileException(string message)
            : base(message) { }
    }

    public class CreateReportException : Exception
    {
        public CreateReportException(string message)
            : base(message) { }
    }

    public class SendEmailException : Exception
    {
        public SendEmailException(string message)
            : base(message) { }
    }
}
