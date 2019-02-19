using SelfService.Code;
using SelfService.Components;
using SelfService.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class Confirm : Form
    {
        static List<ConfirmButton> buttons;

        static Confirm() {

        }

        public static void Box(string message, MessageBoxButtons btns) {
            buttons = new List<ConfirmButton>();
            switch (btns) {
                case MessageBoxButtons.OK:
                    buttons.Add(new ConfirmButton());
                    break;
                case MessageBoxButtons.OKCancel:
                    buttons.Add(new ConfirmButton());
                    buttons.Add(new ConfirmButton(Resources.Cancel, DialogResult.Cancel));
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    buttons.Add(new ConfirmButton(Resources.Abort, DialogResult.Abort));
                    buttons.Add(new ConfirmButton(Resources.Retry, DialogResult.Retry));
                    buttons.Add(new ConfirmButton(Resources.Ignore, DialogResult.Ignore));
                    break;
                case MessageBoxButtons.YesNoCancel:
                    buttons.Add(new ConfirmButton(Resources.Yes, DialogResult.Yes));
                    buttons.Add(new ConfirmButton(Resources.No, DialogResult.No));
                    buttons.Add(new ConfirmButton(Resources.Cancel, DialogResult.Cancel));
                    break;
                case MessageBoxButtons.YesNo:
                    buttons.Add(new ConfirmButton(Resources.Yes, DialogResult.Yes));
                    buttons.Add(new ConfirmButton(Resources.No, DialogResult.No));
                    break;
                case MessageBoxButtons.RetryCancel:
                    buttons.Add(new ConfirmButton(Resources.Retry, DialogResult.Retry));
                    buttons.Add(new ConfirmButton(Resources.Cancel, DialogResult.Cancel));
                    break;
            }

            Panel panel = new Panel {
                Dock = DockStyle.Bottom,
                Height = ConfirmButton.DefaultHeight + 50,
                BackColor = Color.Transparent,
            };
            foreach (var btn in buttons) {
                panel.Controls.Add(btn);
            }
            panel.ControlAdded += (s, e) => {
                e.Control.Click += OnButtonClick;
            };

            Label label = new Label {
                AutoSize = false,
                Text = message,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Width = Screen.PrimaryScreen.Bounds.Width / 2;
            Height = Screen.PrimaryScreen.Bounds.Height / 2;
            BackColor = Color.White;
            Font = new Font(Fonts.DubaiLight, 18f);
            AllowTransparency = true;
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panel);
            Controls.Add(label);
        }

        static void OnButtonClick(object sender, EventArgs e) {
            DialogResult = (sender as ConfirmButton).DialogResult;
            Close();
        }

        public static void Box(string message) {
            Confirm.Box(message, MessageBoxButtons.OK);
        }
    }
}
