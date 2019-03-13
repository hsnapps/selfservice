using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    internal class Map : BaseForm
    {
        public Map() {
            Footer footer = new Footer(Resources.Close);
            footer.SetCallback(0, (s, e) => { Close(); });

            Bitmap map = Tools.LoadImages("Map.png");
            PictureBox box = new PictureBox {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = map
            };

            Controls.AddRange(new Control[] { footer, box });
        }
    }
}