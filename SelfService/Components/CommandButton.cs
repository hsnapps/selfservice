using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SelfService.Components
{
    class CommandButton : Button
    {
        Bitmap button, buttonDown;

        public CommandButton(string text) {
            LoadImages();

            BackgroundImage = button;
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(DefaultWidth, DefaultHeight);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            if (text.EndsWith("*")) {
                Enabled = false;
                Text = text.Substring(0, text.Length - 1);
            } else {
                Text = text; 
            }
            Cursor = Cursors.Hand;
        }

        public CommandButton():this("Button") {}

        void LoadImages() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("SelfService.Images.Button.png");
            button = new Bitmap(stream);

            stream = assembly.GetManifestResourceStream("SelfService.Images.ButtonDown.png");
            buttonDown = new Bitmap(stream);

            stream.Close();
            stream.Dispose();
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            BackgroundImage = buttonDown;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            BackgroundImage = button;
            base.OnMouseUp(mevent);
        }

        public static int DefaultWidth = 420;
        public static int DefaultHeight = 70;
    }
}
