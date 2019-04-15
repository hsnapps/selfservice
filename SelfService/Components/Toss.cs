using SelfService.Code;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Toss : Form
    {
        readonly Timer timer;

        public Toss(string text) {
            Font = new Font(Fonts.ALMohanad, 24f, FontStyle.Regular);

            int width = Screen.PrimaryScreen.Bounds.Width / 3;
            int height = Font.Height + 60;

            StartPosition = FormStartPosition.CenterScreen;
            Text = "";
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ForeColor = Color.White;
            Size = new Size(width, height);
            FormBorderStyle = FormBorderStyle.None;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            Label label = new Label {
                Text = text,
                AutoSize = false,
                Font = this.Font,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Inherit,
            };
            Controls.Add(label);

            timer = new Timer {
                Interval = 300,
                Enabled = true
            };
            timer.Tick += (s, e) => {
                Opacity -= 0.05d;
                if (Opacity <= 0.01) {
                    timer.Stop();
                    Close();
                }
            };
        }
    }
}
