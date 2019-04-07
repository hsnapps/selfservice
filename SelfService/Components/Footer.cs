using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Footer : Panel
    {
        readonly CommandButton[] buttons;

        public Footer(params string[] btns) {
            buttons = new CommandButton[btns.Length];

            for (int i = 0; i < buttons.Length; i++) {
                buttons[i] = new CommandButton(btns[i]) {
                    Name = btns[i],
                    TabStop = false
                };
            }

            var w = Screen.PrimaryScreen.Bounds.Width;
            var x = (w - CommandButton.DefaultWidth) / 2;

            switch (buttons.Length) {
                case 3:
                    buttons[0].Location = new Point(10, 1);
                    buttons[1].Location = new Point(x, 1);
                    buttons[2].Location = new Point(w - 10 - CommandButton.DefaultWidth, 1);
                    break;

                case 2:
                    buttons[0].Location = new Point(10, 1);
                    buttons[1].Location = new Point(w - 10 - CommandButton.DefaultWidth, 1);
                    break;

                default:
                    buttons[0].Location = new Point(x, 1);
                    break;
            }

            Dock = DockStyle.Bottom;
            Height = CommandButton.DefaultHeight + 2;
            BackColor = Color.Transparent;
            Controls.AddRange(buttons);
        }

        public Footer(int height, Padding padding, params string[] btns) : this(btns) {
            Height = height; 
            Padding = padding;
        }

        public void SetCallback(int buttonIndex, EventHandler callback) {
            buttons[buttonIndex].Click += callback;
        }
    }
}
