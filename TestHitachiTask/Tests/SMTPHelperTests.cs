using HitachiTask.Helpers;
using NUnit.Framework;
using System.IO;

namespace HitachiTaskTests
{
    [TestFixture]
    public class SMTPHelperTests
    {
        [Test]
        public void SendEmail()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var fileName = Path.Combine(projectPath, @"Resources\ReportByCountry.csv");
            var sender = "assessment.task0@gmail.com";
            var password = "assessmentTask0*";
            var receiver = "assessment.task0@gmail.com";

            var send = SMTPHelper.SendEmail(sender, password, receiver, fileName);
            Assert.IsTrue(send);
        }
    }
}