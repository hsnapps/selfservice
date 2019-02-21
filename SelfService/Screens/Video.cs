using SelfService.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            player.Bounds = new Rectangle(Padding.Left, Padding.Top, Width - (2 * Padding.Right), Height - (2 * Padding.Bottom));

            string selection = DB.Execute.GetVideoSelection();

            if (selection == "web") {
                player.URL = DB.Execute.GetVideoUrl();
            } else {
                player.URL = DB.Execute.GetVideopath();
            }

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

        private void player_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e) {
            Close();
        }
    }
}
