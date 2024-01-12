namespace Infrastructure.Models.MailModels
{
    public class MailRequest
    {
        public string Receiver { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
