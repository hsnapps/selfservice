using SelfService.Code;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class NoInternetConnection : Form
    {
        Timer checkConnection;
        Bitmap noConnection;
        Bitmap background;

        public NoInternetConnection(){
            InitializeComponent();
            //TopMost = true;
            noConnection = Tools.LoadImage("NoInternet.png");

            checkConnection = new Timer {
                Interval = 1000 * 5,
                Enabled = true,
            };
            checkConnection.Tick += (s, e) => {
                if (Tools.CheckConnection()) {
                    (s as Timer).Stop();
                    Close();
                }
            };
        }

        void InitializeComponent() {
            background = Tools.LoadImage("Background.png");

            SuspendLayout();
            // BaseForm
            AutoScaleDimensions = new SizeF(6F, 13F);
            //RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = background;
            BackgroundImageLayout = ImageLayout.Stretch;
            //ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = Point.Empty;
            Size = Screen.PrimaryScreen.Bounds.Size;
            Name = "BaseForm";
            Text = Resources.TVTC_En_Full;
            ResumeLayout(false);

            string[] args = System.Environment.GetCommandLineArgs();
            bool touch = true;
            foreach (var arg in args) {
                if (arg.StartsWith("--touch=")) {
                    touch = arg.EndsWith("true") || arg.EndsWith("1");
                }
            }

            if (touch) {
                Cursor.Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            using (Font font = new Font(Fonts.ALMohanadBold, 32.0f, FontStyle.Regular)) {
                var height = font.Height + 40;
                var y = (Screen.PrimaryScreen.Bounds.Height - height) / 2;
                y -= 100;
                var layoutRectangle = new Rectangle(0, y, Width, height);
                var format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };
                e.Graphics.DrawString(Resources.NoInternet, font, Brushes.Black, layoutRectangle, format);
                y += height;
                var x = (Width - noConnection.Width) / 2;
                e.Graphics.DrawImage(noConnection, new PointF(x, y));
            }
        }
    }
}
