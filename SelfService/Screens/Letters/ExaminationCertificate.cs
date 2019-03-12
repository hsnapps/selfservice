using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens.Letters
{
    class ExaminationCertificate : BaseForm
    {
        DateInput startDate;
        DateInput endDate;
        Label lblMessage;
        CommandButton print;
        CommandButton close;
        NumaricKeyboard keyboard;

        public ExaminationCertificate() {
            var x = (Screen.PrimaryScreen.Bounds.Width - DateInput.DefaultWidth) / 2;
            var y = 60 + (Screen.PrimaryScreen.Bounds.Height - DateInput.DefaultHeight) / 2;
            var left = new Point(x - (DateInput.DefaultWidth - 150), y - DateInput.DefaultHeight);
            var right = new Point(x + (DateInput.DefaultWidth - 150), y - DateInput.DefaultHeight);
            var loginLocation = new Point(right.X, right.Y + DateInput.DefaultHeight + 20);
            var cancelLocation = new Point(left.X, left.Y + DateInput.DefaultHeight + 20);

            startDate = new DateInput(Resources.ExamStart) {
                Location = right,
                ReadOnly = true
            };
            endDate = new DateInput(Resources.ExamEnd) {
                Location = left,
                ReadOnly = true
            };
            print = new CommandButton(Resources.Print) {
                Location = loginLocation,
            };
            close = new CommandButton(Resources.Close) {
                Location = cancelLocation
            };
            lblMessage = new Label {
                AutoSize = false,
                ForeColor = Color.Red,
                BorderStyle = BorderStyle.None,
                Location = new Point(left.X, close.Location.Y + 100),
                Size = new Size((startDate.Width * 2) + 120, startDate.Height),
                Text = Resources.InvalidCredentials,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                RightToLeft = RightToLeft.Yes,
                Visible = false,
            };
            keyboard = new NumaricKeyboard();

            endDate.InputGotFocus += OnInputGotFocus;
            startDate.InputGotFocus += OnInputGotFocus;
            print.Click += OnPrint;

            close.MouseUp += (s, e) => { Close(); };

            var start = "";
            var end = "";
            DB.Execute.GetExamDuration(ref start, ref end);
            startDate.Date = start;
            endDate.Date = end;

            Controls.Add(startDate);
            Controls.Add(endDate);            
            Controls.Add(print);
            Controls.Add(close);
            Controls.Add(lblMessage);
            Controls.Add(keyboard);
        }

        void OnPrint(object sender, EventArgs e) {
            if (startDate.Year == 0 || startDate.Month == 0 || startDate.Day == 0 ||
                endDate.Year == 0 || endDate.Month == 0 || endDate.Day == 0) {
                return;
            }

            keyboard.Visible = false;
            LetterPrint letter = new LetterPrint(new Documents.ExaminationCertificate(startDate.Date, endDate.Date));
            letter.Show(this);
        }

        void OnInputGotFocus(object sender, EventArgs e) {
            keyboard.Control = (sender as DateInput);
            keyboard.Visible = true;
        }
    }
}
