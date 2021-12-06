using HitachiTask.Contracts;
using System;
using System.Collections.Generic;
using HitachiTask.Helpers;
using System.IO;
using System.Text;

namespace HitachiTask
{
    class Program
    {
        private static InputParams _inputParams = new InputParams();
        private static List<CsvInputDto> _csvInputs = new List<CsvInputDto>();
        private static List<CsvOutputDto> _csvOutputs = new List<CsvOutputDto>();
        static void Main(string[] args)
        {
            try
            {
                GetInputParameters();

                Console.ForegroundColor = ConsoleColor.Blue;

                // get the data from the .csv file
                Console.WriteLine($"{Environment.NewLine}Exctracting the given .csv file...");
                _csvInputs = CSVHelper.ExtractCsvFile(_inputParams.FilePath);

                // generate the new report by country
                Console.WriteLine($"Generating the report...");
                _csvOutputs = ReportHelper.CreateReportByCountry(_csvInputs);

                // create the new .csv file
                Console.WriteLine("Creating the new .csv file...");
                var createdFilePath = CSVHelper.CreateCsvFile(_csvOutputs, "ReportByCountry.csv");

                // send the .csv to the given receiver email address
                Console.WriteLine("Sending the email with the newly created .csv file attacheched...");
                SMTPHelper.SendEmail(_inputParams.SenderEmail, _inputParams.Password, _inputParams.ReceiverEmail, createdFilePath);
                Console.WriteLine("Email was send.");
            }
            catch (Exception ex)
            when (ex is ExtractCsvFileException || ex is CreateCSVFileException
                 || ex is CreateReportException || ex is SendEmailException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception occured while executing the program: {ex.Message}");
                return;
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{Environment.NewLine}Please enter any key to exsit");
                Console.ReadKey();
            }
        }

        #region Input Params

        /// <summary>
        /// get the input parameters for the .csv file 
        /// and the email informations
        /// </summary>
        private static void GetInputParameters()
        {
            ///
            // .csv file path
            Console.WriteLine($"Please add the following information:");
            Console.WriteLine(".csv file path:");
            _inputParams.FilePath = Console.ReadLine();

            // checks if the the given file is a valid one
            while (string.IsNullOrWhiteSpace(_inputParams.FilePath) 
                   || !File.Exists(_inputParams.FilePath)
                   || !Path.GetExtension(_inputParams.FilePath).Equals(".csv"))
            {
                Console.WriteLine($"{Environment.NewLine}Invalid .csv file path/extension. Please input it once again:");
                _inputParams.FilePath = Console.ReadLine();
            }

            ///
            // Sender email address
            Console.WriteLine($"{Environment.NewLine}Sender email address:");
            _inputParams.SenderEmail = Console.ReadLine();
            
            while (string.IsNullOrWhiteSpace(_inputParams.SenderEmail) || !IsValidEmailAddress(_inputParams.SenderEmail))
            {
                Console.WriteLine($"{Environment.NewLine}The given sender email address is invalid. Please input it once again:");
                _inputParams.SenderEmail = Console.ReadLine();
            }

            ///
            // Password
            Console.Write($"{Environment.NewLine}Sender email password:"); 
            StringBuilder passwordBuilder = new StringBuilder();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                char passwordChar = consoleKeyInfo.KeyChar;

                if (passwordChar == newLineChar)
                {
                    continueReading = false;
                }
                else
                {
                    passwordBuilder.Append(passwordChar.ToString());
                }
            }
            _inputParams.Password = passwordBuilder.ToString();

            ///
            //Receiver email address
            Console.WriteLine($"{Environment.NewLine}Receiver email address:");
            _inputParams.ReceiverEmail = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(_inputParams.ReceiverEmail) || !IsValidEmailAddress(_inputParams.ReceiverEmail))
            {
                Console.WriteLine($"{Environment.NewLine}The given receiver email address is invalid. Please input it once again:");
                _inputParams.ReceiverEmail = Console.ReadLine();
            }

        }
        #endregion

        #region helper methods
        /// <summary>
        ///  check if the given email is a valid email address
        /// </summary>
        /// <param name="email">the email address as a string</param>
        private static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var checkedAddress = new System.Net.Mail.MailAddress(email);
                return checkedAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
