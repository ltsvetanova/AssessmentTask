using System;
using System.Net;
using System.Net.Mail;

namespace HitachiTask.Helpers
{
    public class SMTPHelper
    {
        public static bool SendEmail(string from, string password, string to, string fileName)
        {
            try
            {
                MailAddress fromMail = new MailAddress(from);
                MailAddress toMail = new MailAddress(to);

                // create the mail massage with subject, attachment and body
                var mailMassage = new MailMessage(fromMail.Address, toMail.Address);
                Attachment attachment = new Attachment(fileName, new System.Net.Mime.ContentType("text/csv"));
                attachment.Name = "ReportByCountry.csv";
                mailMassage.Attachments.Add(attachment);
                mailMassage.Subject = "Generated report";
                mailMassage.Body = @$"Dear {to},<br /><br /> 
                                  With this email you have received the newly created report.<br />
                                  You can find the new .csv file as an attachment.<br /><br />
                                  Kind regards,<br />{from}";
                mailMassage.IsBodyHtml = true;

                // send the mail
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(from, password),
                    EnableSsl = true
                };
                smtpClient.Send(mailMassage);
                return true;
            }
            catch (Exception ex)
            {
                throw new SendEmailException($"Exception occured while sending an email: {ex.Message}");
            }
        }
    }
}
