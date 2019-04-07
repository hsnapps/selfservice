using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens.Requests
{
    class Maintainance : BaseForm
    {
        readonly DropDown subject;
        readonly Input building;
        readonly Input lab;
        readonly Input details;
        readonly CommandButton send;
        readonly CommandButton close;
        readonly Panel footer;
        readonly FlowLayoutPanel panel;

        public Maintainance() {
            subject = new DropDown(Resources.Subject, "maintainence");
            building = new Input(Resources.Building, true, false) { MaxLength = 5 };
            lab = new Input(Resources.HallOrLab, true, false) { MaxLength = 5 };
            details = new Input(Resources.RequestDetails, true, true) { MaxLength = 250 };

            panel = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 200, 20, 50),
            };
            panel.Controls.AddRange(new Control[] { subject, building, lab, details });
            panel.ControlAdded += (s, e) => {
                (s as FlowLayoutPanel).SetFlowBreak(e.Control, true);
            };

            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            close = new CommandButton(Resources.Close) {
                Location = new Point(x - CommandButton.DefaultWidth - 10, 0),
            };
            close.Click += (s, e) => { Close(); };

            send = new CommandButton(Resources.Send) {
                Location = new Point(x + CommandButton.DefaultWidth + 10, 0),
            };
            send.Click += OnSend;

            footer = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
                BackColor = Color.Transparent,
            };
            footer.Controls.Add(close);
            footer.Controls.Add(send);

            Controls.Add(footer);
            Controls.Add(panel);
        }

        void OnSend(object s, EventArgs e) {
            DB.Execute.Log("requests", "maintainance");
            string _subject = String.Format("{0} - {1}", lab.Text, subject.Text);
            string _body = String.Format("{0}\n{1}", Student.Name_AR, details.Text);

            Mail.Send(To.Maintainance, _subject, _body, this);
        }
    }
}
