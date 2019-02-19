using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens.Letters;
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

            maintainance = new CommandButton(Resources.ToWhomItMayConcern);
            scard = new CommandButton(Resources.SaudiCouncilOfEngineers);
            atm = new CommandButton(Resources.ExaminationCertificate);
            exit = new CommandButton(Resources.Close);

            panel = new FlowLayoutPanel {
                Left = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2,
                Top = 250 + CommandButton.DefaultHeight,
                Width = CommandButton.DefaultWidth + 2,
                Height = CommandButton.DefaultHeight * 6,
                BackColor = Color.Transparent,
            };

            atm.Click += (s, e) => {
                ExaminationCertificate form = new ExaminationCertificate();
                form.Show(this);
            };

            exit.Click += (s, e) => { Close(); };

            panel.Controls.AddRange(new Control[] { maintainance, scard, atm, exit });
            Controls.Add(panel);
        }
    }
}
