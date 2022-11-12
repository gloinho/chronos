namespace Chronos.Domain.Settings
{
    public class EmailSettings
    {
        public string ServerSmtp { get; set; }
        public int ServerPortSmtp { get; set; }
        public string UserSmtp { get; set; }
        public string Password { get; set; }
        public string MailSender { get; set; }
    }
}
