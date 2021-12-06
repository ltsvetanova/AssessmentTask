
namespace HitachiTask.Contracts
{
    public class InputParams
    {
        /// <summary>
        /// The path to the .csv file
        /// </summary>
        public string FilePath      { get; set; }

        /// <summary>
        /// The email address of the sender
        /// </summary>
        public string SenderEmail   { get; set; }

        /// <summary>
        /// The password of the sender's email
        /// </summary>
        public string Password      { get; set; }

        /// <summary>
        /// The email address of the receiver
        /// </summary>
        public string ReceiverEmail { get; set; }

    }
}
