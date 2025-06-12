using System.Net;
using System.Net.Mail;

namespace Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com" , 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("aliaatarek.route42@gmail.com", "fzjttvxgjsgbcawr");
            Client.Send("aliaatarek.route42@gmail.com" , email.To , email.Subject , email.Body);
        }
    }
}
