using SelfService.Code;
using SelfService.Components;
using SelfService.Models;
using SelfService.Properties;
using System;
using System.Collections;
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
        //Timer connectionTimer;
        Image background;
        Keyboard keyboard;

        public BaseForm(bool disableTimer = false, bool checkConnection = true) {
            InitializeComponent();

            if (checkConnection) {
                //connectionTimer = new Timer {
                //    Interval = 1000 * 5,
                //    Enabled = false,
                //};
                //connectionTimer.Tick += (s, e) => {
                //    if (!Tools.CheckConnection()) {
                //        (s as Timer).Stop();
                //        //DisplayNoConnection();
                //    }
                //};
            }

            timer = new Timer {
                Interval = DB.Execute.GetTimeout() * 1000,
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

            //KeyUp += (s, e) => {
            //    base.OnKeyUp(e);
            //};

            //keyboard = new Keyboard();
            //keyboard.Hide();
        }

        void InitializeComponent() {
            background = Code.Settings.Background;

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
            KeyPreview = true;
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

            keyboard = new Keyboard();
            keyboard.Show();
            keyboard.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (BaseForm.Student != null) {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(1, 39, 94))) {
                    var size = new Size(Screen.PrimaryScreen.Bounds.Width / 2, 70);
                    var x = (Screen.PrimaryScreen.Bounds.Width / 2) - (size.Width / 2);
                    var point = new Point(x, 20);
                    var layoutRectangle = new RectangleF(point, size);
                    var shadowRectangle = new RectangleF(new Point(point.X + 3, point.Y + 3), size);

                    using (SolidBrush shadow = new SolidBrush(Color.FromArgb(128, 128, 128))) {
                        e.Graphics.FillRectangle(shadow, shadowRectangle);
                    }

                    e.Graphics.FillRectangle(brush, layoutRectangle);

                    using (Font font = new Font("Arial", 22f, FontStyle.Bold)) {
                        var format = new StringFormat {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                            FormatFlags = StringFormatFlags.DirectionRightToLeft
                        };

                        var name = String.Format(Resources.Welcome, BaseForm.Student.Name_AR);
                        e.Graphics.DrawString(name, font, Brushes.White, layoutRectangle, format);
                    } 
                }
            }
        }

#if DEBUG
        //protected override void OnFormClosing(FormClosingEventArgs e) {
        //    MessageBox.Show(e.CloseReason.ToString());
        //} 

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.K) {
                Keyboard keyboard = new Keyboard();
                keyboard.Show();
            }
            base.OnKeyDown(e);
        }
#endif

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
        public Keyboard Keyboard { get => keyboard; }
    }
}
