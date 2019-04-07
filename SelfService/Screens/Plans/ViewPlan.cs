using SelfService.Components;
using SelfService.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens.Plans
{
    class ViewPlan : BaseForm
    {
        WebBrowser web;
        CommandButton button, prev, next;
        Panel panel;

        public ViewPlan(string pdf) {
            web = new WebBrowser {
                Dock = DockStyle.Fill,
            };
            web.Navigate(pdf + "#toolbar=0&navpanes=1&scrollbar=1");

            button = new CommandButton(Resources.Close) {
                Location = new Point(5, 1),
                TabStop = false,
            };
            prev = new CommandButton(">") {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth - 5), 1),
                TabStop = false,
            };
            next = new CommandButton("<") {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth - 5) - prev.Width, 1),
                TabStop = false,
            };

            button.Click += (s, e) => { this.Close(); };
            prev.Click += (s, e) => {
                web.Focus();
                Application.DoEvents();
                SendKeys.Send("{PGUP}");
            };
            next.Click += (s, e) => {
                web.Focus();
                Application.DoEvents();
                SendKeys.Send("{PGDN}");
            };

            panel = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            panel.Controls.Add(button);
            panel.Controls.Add(prev);
            panel.Controls.Add(next);

            this.Controls.Add(panel);
            //this.Controls.Add(pic);
            this.Controls.Add(web);
        }
    }
}
