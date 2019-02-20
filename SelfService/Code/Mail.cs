using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
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

        public static void Send(To to, string subject, string body, BaseForm form = null) {
            string email = DB.Execute.GetEmail(Enum.GetName(typeof(To), to).ToLower());            
            string username = DB.Execute.GetEmail("username");
            string password = DB.Execute.GetEmail("password");
            string displayName = DB.Execute.GetEmail("displayName");
            string host = DB.Execute.GetEmail("host");
            int port = Convert.ToInt32(DB.Execute.GetEmail("port"));
            int timeout = 50000;

            Waiting waiting = new Waiting(form);
            waiting.Show();

            SmtpClient client = new SmtpClient {
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = host,
                EnableSsl = false,
                Credentials = new NetworkCredential(username, password),
                Timeout = timeout,
            };
            MailMessage mail = new MailMessage(BaseForm.Student.Email, email) {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            try {
                client.SendCompleted += OnSendCompleted;
                client.SendAsync(mail, waiting);
                //MessageBox.Show(Resources.SentSuccessfully);
                //waiting.Close();
            } catch (Exception x) {
                //waiting.Close();
                MessageBox.Show(Resources.SendingError + "\n" + x.Message);
            }

            //waiting.Dispose();
        }

        static void OnSendCompleted(object sender, AsyncCompletedEventArgs e) {
            if (e.Error != null) {
                (e.UserState as Waiting).ShowMessage(e.Error.Message);
            } else {
                (e.UserState as Waiting).ShowMessage(Resources.SentSuccessfully); 
            }
        }
    }
}
