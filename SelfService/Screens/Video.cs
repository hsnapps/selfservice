using SelfService.Properties;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SelfService.Screens
{
    public partial class Video : Form
    {
        public Video() {
            InitializeComponent();

            AutoScaleDimensions = new SizeF(6F, 13F);
            RightToLeftLayout = true;
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = LoadImages();
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = Point.Empty;
            Size = Screen.PrimaryScreen.Bounds.Size;
            Name = "BaseForm";
            Text = Resources.TVTC_En_Full;

            player.Bounds = this.Bounds; //;new Rectangle(Padding.Left, Padding.Top, Width - (2 * Padding.Right), Height - (2 * Padding.Bottom));
            
            string url = "";
            string selection = DB.Execute.GetVideoSelection();
            switch (selection) {
                case "web":
                    url = DB.Execute.GetVideoUrl();
                    break;
                case "youtube":
                    url = DB.Execute.GetYoutubeUrl();
                    break;
                default:
                    url = DB.Execute.GetVideoPath();
                    break;
            }

            player.URL = url;
            this.Click += (s, e) => { Close(); };
        }

        Bitmap LoadImages() {
            Bitmap background = null;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SelfService.Images.Background.png")) {
                background = new Bitmap(stream);
                stream.Close();
            }
            return background;
        }

        private void OnPlayerClick(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e) {
            Close();
        }
    }
}
