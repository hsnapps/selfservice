using SelfService.Components;
using SelfService.Properties;
using SelfService.SelfService.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Plan : BaseForm
    {
        readonly Panel footer;
        readonly CommandButton close;
        readonly CommandButton back;
        readonly List<string> screens;
        PlanPanel panel;

        public Plan() {
            screens = DB.Execute.ReadPlans();
            panel = new PlanPanel(screens, PanelButtonClick);

            close = new CommandButton(Resources.Close) {
                Location = new Point(5, 1),
                TabStop = false,
            };
            close.Click += (s, e) => { this.Close(); };

            back = new CommandButton(Resources.Back) {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth - 5), 1),
                TabStop = false,
                Name = "back",
            };
            back.Click += (s, e) => {
            };

            footer = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            footer.Controls.Add(close);
            footer.Controls.Add(back);

            Controls.Add(panel);
            Controls.Add(footer);
        }

        void PanelButtonClick(object s, string tag) {
            panel.LoadButtons(tag, PanelButtonClick);
        }
    }
}
