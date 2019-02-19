using SelfService.Components;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Requests : BaseForm
    {
        CommandButton maintainance, scard, atm, car, exit;
        FlowLayoutPanel panel;

        public Requests() {
            var x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;

            maintainance = new CommandButton(Resources.RequestMaintainance);
            scard = new CommandButton(Resources.RequestStudentCard);
            atm = new CommandButton(Resources.RequestATMCard);
            car = new CommandButton(Resources.RequestCarLicense);
            exit = new CommandButton(Resources.Close);

            panel = new FlowLayoutPanel {
                Left = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2,
                Top = 250 + CommandButton.DefaultHeight,
                Width = CommandButton.DefaultWidth + 2,
                Height = CommandButton.DefaultHeight * 6,
                BackColor = Color.Transparent,
            };

            maintainance.Click += (s, e) => {
                MaintainanceRequest request = new MaintainanceRequest();
                request.Show(this);
            };

            exit.Click += (s, e) => { Close(); };

            panel.Controls.AddRange(new Control[] { maintainance, scard, atm, exit });
            Controls.Add(panel);
        }
    }
}
