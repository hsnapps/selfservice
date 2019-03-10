using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class StudentGuide : BaseForm
    {
        WebBrowser web;
        CommandButton button, prev, next;
        Panel panel;
        PictureBox pic;

        public StudentGuide() {
            web = new WebBrowser {
                Dock = DockStyle.Fill,
            };
            var path = Application.StartupPath + @"\Pdf\StudentGuide.pdf?toolbar=0&navpanes=0&scrollbar=0";
            web.Navigate(path);

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
                SendKeys.Send("{TAB}{PGUP}");
            };
            next.Click += (s, e) => {
                SendKeys.Send("{TAB}{PGDN}");
            };

            pic = new PictureBox {
                Dock = DockStyle.Fill,
                Tag = path,
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

        protected override void OnLoad(EventArgs e) {
            //
        }
    }
}
