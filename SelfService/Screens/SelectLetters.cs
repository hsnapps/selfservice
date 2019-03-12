using SelfService.Components;
using SelfService.Documents;
using SelfService.Properties;
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

            toWhomItMayConcern = new CommandButton(Resources.ToWhomItMayConcernLetter);
            saudiCouncilOfEngineers = new CommandButton(Resources.SaudiCouncilOfEngineersLetter);
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
                DB.Execute.Log("letters", "examinationCertificate");
                var start = "";
                var end = "";
                DB.Execute.GetExamDuration(ref start, ref end);
                LetterPrint letter = new LetterPrint(new Documents.ExaminationCertificate(start, end));
                letter.Show(this);
            };
            saudiCouncilOfEngineers.Click += (s, e) => {
                DB.Execute.Log("letters", "saudicouncilofengineers");
                var letter = new LetterPrint(new LetterOfCertificate(Resources.SaudiCouncilOfEngineers));
                letter.Show();
            };
            toWhomItMayConcern.Click += (s, e) => {
                DB.Execute.Log("letters", "towhomitmayconcern");
                var letter = new LetterPrint(new LetterOfCertificate(Resources.ToWhomItMayConcern));
                letter.Show();
            };

            exit.Click += (s, e) => { Close(); };

            panel.Controls.AddRange(new Control[] { toWhomItMayConcern, saudiCouncilOfEngineers, examinationCertificate, exit });
            Controls.Add(panel);
        }
    }
}
