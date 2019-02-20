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
        Bitmap background;
        Keyboard keyboard;

        public BaseForm(bool disableTimer = false) {
            //LoadFonts();
            LoadImages();
            InitializeComponent();

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

        protected void ShowKeyboard(bool show) {
            //keyboard.Visible = show;
        }

        void LoadImages() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SelfService.Images.Background.png")) {
                background = new Bitmap(stream);
                stream.Close();
            }
        }

        void InitializeComponent() {
            timer = new Timer {
                Interval = DB.Execute.GetTimeout(),
                Enabled = true,
            };
            timer.Tick += (s, e) => {
                IsTimeout = true;
                Close();
            };

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

#if !DEBUG
            Cursor.Hide(); 
#endif
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
