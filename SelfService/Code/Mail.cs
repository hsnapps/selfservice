using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SelfService.Code
{
    public enum To
    {
        Maintainance
    }
    static class Mail
    {
        static Mail() {

        }

        public static async Task Send(To to, string subject, string body) {
            string email = "";
            switch (to) {
                case To.Maintainance:
                    email = DB.Execute.GetEmail("maintainence");
                    break;
            }
            string from = DB.Execute.GetEmail("from");
            string password = DB.Execute.GetEmail("password");
            string displayName = DB.Execute.GetEmail("displayName");

            SmtpClient client = new SmtpClient {
                Port = Convert.ToInt32(DB.Execute.GetEmail("port")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = DB.Execute.GetEmail("host"),
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(from, password),
                Timeout = 5 * 1000,
            };
            MailMessage mail = new MailMessage(from, email) {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            try {
                await client.SendMailAsync(mail);
            } catch (Exception) {

            }
        }
    }
}
