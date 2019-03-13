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

        public StudentGuide() {
            web = new WebBrowser {
                Dock = DockStyle.Fill,
            };
            var path = Application.StartupPath + @"\Pdf\StudentGuide.pdf?toolbar=0&navpanes=0&scrollbar=0";
            web.Navigate(path);

            Footer footer = new Footer(Resources.Close, "<", ">");
            footer.SetCallback(0, (s, e) => { this.Close(); });
            footer.SetCallback(1, (s, e) => { web.Focus(); SendKeys.Send("{PGDN}"); });
            footer.SetCallback(2, (s, e) => { web.Focus(); SendKeys.Send("{PGDN}"); });

            Controls.Add(footer);
            Controls.Add(web);
        }
    }
}
