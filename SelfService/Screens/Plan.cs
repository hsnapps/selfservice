using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens.Plans;
using SelfService.SelfService.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Plan : BaseForm
    {
        PlanPanel panel;
        Stack<string> screensStack;
        string currentScreen;

        public Plan() {
            panel = new PlanPanel("", PanelButtonClick);

            CommandButton close = new CommandButton(Resources.Close) {
                Location = new Point(5, 1),
                TabStop = false,
            };
            close.Click += (s, e) => { this.Close(); };

            screensStack = new Stack<string>();
            CommandButton back = new CommandButton(Resources.Back) {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth - 5), 1),
                TabStop = false,
                Name = "back",
            };
            back.Click += (s, e) => {
                if (screensStack.Count == 0) {
                    return;
                }
                currentScreen = screensStack.Pop();
                panel.LoadButtons(currentScreen, PanelButtonClick);
            };

            Panel footer = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            footer.Controls.Add(close);
            footer.Controls.Add(back);

            Controls.Add(panel);
            Controls.Add(footer);
        }

        void PanelButtonClick(object s, string screen) {
            if (screen.EndsWith(".pdf")) {
                var view = new ViewPlan(screen);
                view.Show(this);
                return;
            }
            screensStack.Push(currentScreen);
            currentScreen = screen;
            panel.LoadButtons(screen, PanelButtonClick);
        }
    }
}
