using SelfService.Code;
using SelfService.Properties;
using SelfService.Screens;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Waiting : Form
    {
        readonly PictureBox picture;
        readonly Label label;
        readonly CommandButton button;
        readonly BaseForm form;

        public Waiting(BaseForm form = null) {
            Size = Screen.PrimaryScreen.Bounds.Size;
            Location = Screen.PrimaryScreen.Bounds.Location;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font(Fonts.HeshamAlSharq, 25);

            this.form = form;

            label = new Label {
                AutoSize = false,
                TextAlign = ContentAlignment.BottomCenter,                
                Location = new Point(0, 100),
                Size = new Size(this.Width, (this.Height / 3) / 2),
                Visible = false,
            };

            button = new CommandButton(Resources.Close) {
                Location = new Point((this.Width - CommandButton.DefaultWidth) / 2, label.Bounds.Bottom + 20),
                Visible = false,
            };
            button.Click += (s, e) => {
                if (form != null) {
                    form.Close();
                }
                Close();
            };

            picture = new PictureBox {
                Dock = DockStyle.Fill,
                Name = "picture",
                SizeMode = PictureBoxSizeMode.CenterImage,
                TabStop = false,
                Image = (Image)Resources.Waiting_2,
            };

            Controls.Add(picture);
            Controls.Add(label);
            Controls.Add(button);
        }

        internal void ShowMessage(string message) {
            picture.Visible = false;
            label.Text = message;
            label.Visible = true;
            button.Visible = true;
        }
    }
}
