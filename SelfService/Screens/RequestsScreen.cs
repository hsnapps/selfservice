using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens.Requests;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class RequestsScreen : BaseForm
    {
        CommandButton maintainance, scard, atm, car, exit;
        FlowLayoutPanel panel;

        public RequestsScreen() {
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

            maintainance.Click += OnButtonClick;
            scard.Click += OnButtonClick;
            atm.Click += OnButtonClick;
            car.Click += OnButtonClick;
            exit.Click += OnButtonClick;

            panel.Controls.AddRange(new Control[] { maintainance, scard, atm, exit });
            Controls.Add(panel);
        }

        void OnButtonClick(object s, EventArgs e) {
            CommandButton b = (s as CommandButton);

            if (b.Text == Resources.RequestMaintainance) {
                Maintainance maintainance = new Maintainance();
                maintainance.Show(this);
            } else if (b.Text == Resources.RequestStudentCard) {
                StudentCard studentCard = new StudentCard();
                studentCard.Show(this);
            } else if (b.Text == Resources.RequestATMCard) {
                ATMCard card = new ATMCard();
                card.Show();
            } else if (b.Text == Resources.RequestCarLicense) {

            } else {
                Close();
            }
        }
    }
}
