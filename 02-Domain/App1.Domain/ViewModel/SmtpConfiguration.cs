namespace App1.Domain.ViewModel
{
    public class SmtpConfiguration
    {
        public string ApplicationUrl {get;set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
}