using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens.Requests
{
    class CarBadge : BaseForm
    {
        readonly Input carModel;
        readonly Input paletLetters;
        readonly Input paletNumbers;
        readonly Label label;
        readonly CommandButton send;
        readonly CommandButton close;
        readonly Panel footer;
        readonly FlowLayoutPanel panel;

        public CarBadge() {
            carModel = new Input(Resources.CarModel, true, false);
            paletLetters = new Input(Resources.PaletLetters, true, false) { MaxLength = 3 };
            paletNumbers = new Input(Resources.PaletNumbers, true, false) { MaxLength = 4 };
            label = new Label {
                Font = new Font(Fonts.ALMohanadBold, 25),
                AutoSize = false,
                Size = carModel.Size,
                Text = Resources.PaletNote,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            panel = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 150, 20, 50),
            };
            panel.Controls.AddRange(new Control[] { carModel, label, paletLetters, paletNumbers });
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
            DB.Execute.Log("letters", "car");
            string subject = Resources.RequestCarLicense;
            string body = Resources.CarData
                .Replace("<id>", BaseForm.Student.ID)
                .Replace("<mobile>", BaseForm.Student.Mobile)
                .Replace("<name_ar>", BaseForm.Student.Name_AR)
                .Replace("<name_en>", BaseForm.Student.Name_EN)
                .Replace("<id_num>", BaseForm.Student.ID_Number)
                .Replace("<carModel>", carModel.Text)
                .Replace("<paletNumbers>", paletNumbers.Text)
                .Replace("<paletLetters>", paletLetters.Text);

            Mail.Send(To.CarBadge, subject, body, this);
        }
    }
}
