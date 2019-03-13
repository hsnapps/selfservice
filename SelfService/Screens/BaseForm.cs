using SelfService.Code;
using SelfService.Components;
using SelfService.Models;
using SelfService.Properties;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class BaseForm : Form
    {
        Timer timer;
        Timer connectionTimer;
        Bitmap background;
        NoInternetConnection noInternetConnection;
        //Keyboard keyboard;

        public BaseForm(bool disableTimer = false, bool checkConnection = true) {
            InitializeComponent();

            if (checkConnection) {
                connectionTimer = new Timer {
                    Interval = 1000 * 5,
                    Enabled = true,
                };
                connectionTimer.Tick += (s, e) => {
                    if (!Tools.CheckConnection()) {
                        (s as Timer).Stop();
                        DisplayNoConnection();
                    }
                };
            }

            timer = new Timer {
                Interval = DB.Execute.GetTimeout() * 60 * 1000,
                Enabled = true,
            };
            timer.Tick += (s, e) => {
                IsTimeout = true;
                Close();
            };

            if (disableTimer) {
                DisableTimer();
            }

            Font = new Font(Fonts.HeshamAlSharq, 22f);

            KeyUp += (s, e) => {
                base.OnKeyUp(e);
            };

            //keyboard = new Keyboard();
            //keyboard.Hide();
        }

        void DisplayNoConnection() {
            noInternetConnection = new NoInternetConnection();
            noInternetConnection.FormClosed += (s1, e1) => {
                connectionTimer.Start();
            };
            noInternetConnection.Show();
        }

        protected void ShowKeyboard(bool show) {
            //keyboard.Visible = show;
        }

        void InitializeComponent() {
            background = Tools.LoadImages("Background.png");

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
            base.OnPaint(e);
            if (BaseForm.Student != null) {
                var font = new Font("Arial", 18f, FontStyle.Bold);
                var layoutRectangle = new RectangleF(new Point(0, 20), new Size(Screen.PrimaryScreen.Bounds.Width, 30));
                var format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };
                e.Graphics.DrawString(BaseForm.Student.Name_AR, font, Brushes.RoyalBlue, layoutRectangle, format);
            }
        }

        public void ResetTimer() {
            timer.Stop();
            timer.Start();
        }

        public void DisableTimer() {
            timer.Stop();
            timer.Dispose();
        }

        public bool IsTimeout { get; set; }
        public static Student Student { get; set; }
    }
}
