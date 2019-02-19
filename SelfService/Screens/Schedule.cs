using SelfService.Components;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Schedule : BaseForm
    {
        CommandButton close, print;
        Panel panel;

        public Schedule() {
            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            close = new CommandButton(Resources.Close) {
                Location = new Point(x - CommandButton.DefaultWidth - 10, 0),
            };
            close.Click += (s, e) => { Close(); };

            print = new CommandButton(Resources.Print) {
                Location = new Point(x + CommandButton.DefaultWidth + 10, 0),
            };
            //print.Click += (s, e) => {};

            panel = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            panel.Controls.Add(close);
            panel.Controls.Add(print);

            Controls.Add(panel);
        }
    }
}
