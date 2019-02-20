using SelfService.Properties;
using SelfService.Screens;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Code
{
    public enum To
    {
        Maintainance,
        Admission
    }
    static class Mail
    {
        static Mail() {

        }

        public static void Send(To to, string subject, string body) {
            string email = DB.Execute.GetEmail(Enum.GetName(typeof(To), to).ToLower()); ;
            //switch (to) {
            //    case To.Maintainance:
            //        email = DB.Execute.GetEmail("maintainence");
            //        break;
            //    case To.Admission:
            //        email = DB.Execute.GetEmail("admission");
            //        break;
            //}
            string username = DB.Execute.GetEmail("username");
            string password = DB.Execute.GetEmail("password");
            string displayName = DB.Execute.GetEmail("displayName");
            string host = DB.Execute.GetEmail("host");
            int port = Convert.ToInt32(DB.Execute.GetEmail("port"));

            SmtpClient client = new SmtpClient {
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = host,
                EnableSsl = false,
                Credentials = new NetworkCredential(username, password),
                Timeout = 50000,
            };
            MailMessage mail = new MailMessage(BaseForm.Student.Email, email) {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            try {
                client.Send(mail);
                MessageBox.Show(Resources.SentSuccessfully);
            } catch (Exception x) {
                MessageBox.Show(Resources.SendingError + "\n" + x.Message);
            }
        }
    }
}
