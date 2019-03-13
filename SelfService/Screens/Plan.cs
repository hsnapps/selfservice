using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens.Plans;
using SelfService.SelfService.Components;
using System.Collections.Generic;

namespace SelfService.Screens
{
    class Plan : BaseForm
    {
        PlanPanel panel;
        Stack<string> screensStack;
        string currentScreen;

        public Plan() {
            panel = new PlanPanel("", PanelButtonClick);
            screensStack = new Stack<string>();

            Footer footer = new Footer(Resources.Close, Resources.Back);
            footer.SetCallback(0, (s, e) => { this.Close(); });
            footer.SetCallback(1, (s, e) => {
                if (screensStack.Count == 0) {
                    return;
                }
                currentScreen = screensStack.Pop();
                panel.LoadButtons(currentScreen, PanelButtonClick);
            });

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
