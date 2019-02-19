using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens.Letters;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class SelectLetters : BaseForm
    {
        CommandButton toWhomItMayConcern;
        CommandButton saudiCouncilOfEngineers;
        CommandButton examinationCertificate;
        CommandButton exit;
        FlowLayoutPanel panel;

        public SelectLetters() {
            var x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            //var y = 250;

            toWhomItMayConcern = new CommandButton(Resources.ToWhomItMayConcern);
            saudiCouncilOfEngineers = new CommandButton(Resources.SaudiCouncilOfEngineers);
            examinationCertificate = new CommandButton(Resources.ExaminationCertificate);
            exit = new CommandButton(Resources.Close);
            panel = new FlowLayoutPanel {
                Left = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2,
                Top = 250 + CommandButton.DefaultHeight,
                Width = CommandButton.DefaultWidth + 2,
                Height = CommandButton.DefaultHeight * 5,
                BackColor = Color.Transparent,
            };

            examinationCertificate.Click += (s, e) => {
                ExaminationCertificate form = new ExaminationCertificate();
                form.Show(this);
            };

            exit.Click += (s, e) => {
                this.Close();
            };

            panel.Controls.AddRange(new Control[] { toWhomItMayConcern, saudiCouncilOfEngineers, examinationCertificate, exit });
            this.Controls.Add(panel);
        }
    }
}
