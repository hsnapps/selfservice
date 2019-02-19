using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class ConfirmButton : Button
    {
        public ConfirmButton(string text, DialogResult result) {
            Text = text;
            Width = (Screen.PrimaryScreen.Bounds.Width / 2) / 3;
            Height = 50;
            BackColor = Color.Black;
            ForeColor = Color.White;
            DialogResult = result;
        }
        public ConfirmButton() : this(Resources.OK, DialogResult.OK) { }

        public static int DefaultHeight { get => 50; }
    }
}
