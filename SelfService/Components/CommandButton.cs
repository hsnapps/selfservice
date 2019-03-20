using SelfService.Code;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class CommandButton : Button
    {
        Image buttonUpImage, buttonDownImage;

        public CommandButton(string text) {
            LoadImages(text);

            BackgroundImage = buttonUpImage;
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(DefaultWidth, DefaultHeight);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font(Fonts.HeshamAlSharq, 22.0f);
            if (text.EndsWith("*")) {
                Enabled = false;
                Text = text.Substring(0, text.Length - 1);
            } else {
                Text = text; 
            }
            if (Text.Length > 26) {
                Font = new Font(Fonts.HeshamAlSharq, 18.0f);
            }
            Cursor = Cursors.Hand;
        }

        public CommandButton():this("Button") {}

        void LoadImages(string text) {
            if (text.Equals(Resources.Back) || text.Equals(Resources.Exit) || text.Equals(Resources.Close)) {
                buttonUpImage = Code.Settings.BackButtonUpImage;
                buttonDownImage = Code.Settings.BackButtonDnImage;
                ForeColor = Code.Settings.BackButtonForeColor;
            } else {
                buttonUpImage = Code.Settings.ButtonUpImage;
                buttonDownImage = Code.Settings.ButtonDownImage;
                ForeColor = Code.Settings.ButtonForeColor;
            }            
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            BackgroundImage = buttonDownImage;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            BackgroundImage = buttonUpImage;
            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e) {
            ForeColor = Enabled ? Color.Black : Color.FromArgb(226, 226, 226);
        }

        public static int DefaultWidth = 420;
        public static int DefaultHeight = 70;
    }
}
