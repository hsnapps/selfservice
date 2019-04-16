using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens.Requests
{
    class StudentCard : BaseForm
    {
        readonly DropDown reason;
        //readonly CommandButton send;
        //readonly CommandButton close;
        //readonly Panel footer;
        readonly FlowLayoutPanel panel;

        public StudentCard() {
            reason = new DropDown(Resources.CardReason, "reason");
            panel = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 400, 20, 50),
            };
            panel.Controls.AddRange(new Control[] { reason });
            panel.ControlAdded += (s, e) => {
                (s as FlowLayoutPanel).SetFlowBreak(e.Control, true);
            };

            //int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            //close = new CommandButton(Resources.Close) {
            //    Location = new Point(x - CommandButton.DefaultWidth - 10, 0),
            //};
            //close.Click += (s, e) => { Close(); };

            //send = new CommandButton(Resources.Send) {
            //    Location = new Point(x + CommandButton.DefaultWidth + 10, 0),
            //};
            //send.Click += OnSend;

            //footer = new Panel {
            //    Dock = DockStyle.Bottom,
            //    Height = CommandButton.DefaultHeight + 2,
            //    BackColor = Color.Transparent,
            //};
            //footer.Controls.Add(close);
            //footer.Controls.Add(send);

            Footer footer = new Footer(CommandButton.DefaultHeight + 120, new Padding(0, 5, 0, 5), Resources.Back, Resources.Send);
            footer.SetCallback(0, (s, e) => { Close(); });
            footer.SetCallback(1, OnSend);

            Controls.Add(footer);
            Controls.Add(panel);
        }

        void OnSend(object s, EventArgs e) {
            DB.Execute.Log("requests", "scard");
            string subject = Resources.RequestStudentCard + " - " + reason.Text;
            string body = Resources.StudentData
                .Replace("<id>", BaseForm.Student.ID)
                .Replace("<mobile>", BaseForm.Student.Mobile)
                .Replace("<name_ar>", BaseForm.Student.Name_AR)
                .Replace("<name_en>", BaseForm.Student.Name_EN)
                .Replace("<id_num>", BaseForm.Student.ID_Number)
                .Replace("<program>", BaseForm.Student.Program)
                .Replace("<section>", BaseForm.Student.Section)
                .Replace("<level>", BaseForm.Student.Level);

            Mail.Send(To.StudentCard, subject, body, this);
        }
    }
}
