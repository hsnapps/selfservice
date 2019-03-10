using SelfService.Code;
using SelfService.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.SelfService.Components
{
    class PlanPanel : FlowLayoutPanel
    {
        Dictionary<string, string> buttons;

        public PlanPanel(string screen, PlanClickCallback callback) {
            Dock = DockStyle.Fill;
            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            Padding = new Padding(x, 150, 0, 0);
            Name = screen;

            LoadButtons(screen, callback);
        }

        public void LoadButtons(string screen, PlanClickCallback callback) {
            Controls.Clear();

            buttons = DB.Execute.ReadPlanButtons(screen);
            foreach (var button in buttons) {
                CommandButton command = new CommandButton(button.Key) { Tag = button.Value };
                command.MouseUp += OnCommandClick;
                Controls.Add(command);
            }

            PlanClick += callback;
        }

        public PlanPanel(List<string> screens, PlanClickCallback callback) {
            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;
            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            Padding = new Padding(x, 150, 0, 0);
            Name = "الخطط التدريبية";

            foreach (var button in screens) {
                CommandButton command = new CommandButton(button) { Tag = button };
                command.MouseUp += OnCommandClick;
                Controls.Add(command);
            }

            PlanClick += callback;
        }

        protected override void OnControlAdded(ControlEventArgs e) {
            e.Control.Margin = new Padding(10);
            SetFlowBreak(e.Control, true);
        }

        public override string ToString() {
            return Name;
        }

        void OnCommandClick(object sender, MouseEventArgs e) {
            PlanClick?.Invoke(this, Convert.ToString((sender as CommandButton).Tag));
        }

        public event PlanClickCallback PlanClick;
    }
}
