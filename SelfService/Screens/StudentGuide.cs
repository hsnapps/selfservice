using SelfService.Components;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class StudentGuide : BaseForm
    {
        WebBrowser web;
        CommandButton button;
        Panel panel;

        public StudentGuide() {
            web = new WebBrowser {
                Dock = DockStyle.Fill,
            };
            var path = Application.StartupPath + @"\Pdf\StudentGuide.pdf";
            web.Navigate(path);

            button = new CommandButton(Resources.Exit) {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2, 1),
            };
            button.Click += (s, e)=> {
                this.Close();
            };

            panel = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            panel.Controls.Add(button);

            this.Controls.Add(panel);
            this.Controls.Add(web);
        }
    }
}
