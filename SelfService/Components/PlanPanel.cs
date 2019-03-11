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
        bool setFlowBreak;

        public PlanPanel(string screen, PlanClickCallback callback) {
            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;
            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            Name = screen;
            FlowDirection = FlowDirection.RightToLeft;

            LoadButtons(screen, callback);
        }

        public void LoadButtons(string screen, PlanClickCallback callback) {
            Controls.Clear();

            buttons = DB.Execute.ReadPlanButtons(screen);
            setFlowBreak = buttons.Count < 6;
            if (setFlowBreak) {
                int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
                Padding = new Padding(x, 250, 0, 0);
            } else {
                Padding = new Padding(50, 150, 50, 0);
            }
            foreach (var button in buttons) {
                string text = Tools.ReadPlanResource(button.Key);
                CommandButton command = new CommandButton(text) { Tag = button.Value };
                command.MouseUp += OnCommandClick;
                Controls.Add(command);
            }

            if (PlanClick == null) {
                PlanClick += callback; 
            }
        }

        protected override void OnControlAdded(ControlEventArgs e) {
            e.Control.Margin = new Padding(10);
            if(setFlowBreak) SetFlowBreak(e.Control, true);
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
