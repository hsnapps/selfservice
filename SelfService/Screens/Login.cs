using System;
using System.Drawing;
using System.Windows.Forms;
using SelfService.Components;
using SelfService.Models;
using SelfService.Properties;

namespace SelfService.Screens
{
    class Login : BaseForm
    {
        Input traineeNumber;
        Input idNumber;
        Label lblMessage;
        CommandButton login;
        CommandButton cancel;
        NumaricKeyboard keyboard;

        public Login() {
            var x = (Screen.PrimaryScreen.Bounds.Width - Input.DefaultWidth) / 2;
            var y = 60 + (Screen.PrimaryScreen.Bounds.Height - Input.DefaultHeight) / 2;
            var left = new Point(x - (Input.DefaultWidth - 150), y - Input.DefaultHeight);
            var right = new Point(x + (Input.DefaultWidth - 150), y - Input.DefaultHeight);
            var loginLocation = new Point(right.X, right.Y + Input.DefaultHeight + 20);
            var cancelLocation = new Point(left.X, left.Y + Input.DefaultHeight + 20);

            traineeNumber = new Input(Resources.TraineeNumber) {
                Location = right,
            };
            idNumber = new Input(Resources.IdNumber, true) {
                Location = left,
            };
            login = new CommandButton(Resources.Login) {
                Location = loginLocation,
            };
            cancel = new CommandButton(Resources.Cancel) {
                Location = cancelLocation
            };
            lblMessage = new Label {
                AutoSize = false,
                ForeColor = Color.Red,
                BorderStyle = BorderStyle.None,
                Location = new Point(left.X, cancel.Location.Y + 100),
                Size = new Size((traineeNumber.Width * 2) + 120, traineeNumber.Height),
                Text = Resources.InvalidCredentials,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                RightToLeft = RightToLeft.Yes,
                Visible = false,
            };
            keyboard = new NumaricKeyboard();

#if DEBUG
            idNumber.Text = "1087534176";
            traineeNumber.Text = "114343617";
#endif

            idNumber.InputGotFocus += OnInputGotFocus;
            traineeNumber.InputGotFocus += OnInputGotFocus;

            cancel.MouseUp += (s, e) => {
                this.Close();
            };

            login.MouseUp += OnLogin;

            this.Controls.Add(traineeNumber);
            this.Controls.Add(idNumber);
            this.Controls.Add(login);
            this.Controls.Add(cancel);
            this.Controls.Add(lblMessage);
            this.Controls.Add(keyboard);
        }

        void OnInputGotFocus(object sender, EventArgs e) {
            keyboard.Control = (sender as Input);
            keyboard.Visible = true;
            //base.ShowKeyboard(true);
        }

        void OnLogin(object sender, MouseEventArgs e) {
            Student student = DB.Execute.Login(traineeNumber.Text, idNumber.Text);
            if (student == null) {
                lblMessage.Visible = true;
                return;
            }

            BaseForm.Student = student;
            IsLogged = true;
            this.Close();
        }

        public bool IsLogged { get; set; }
    }
}
