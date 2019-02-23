using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SelfService.Screens
{
    class LetterPrint : BaseForm
    {
        readonly PrintPreviewControl preview;
        readonly CommandButton close, print;
        readonly Panel panel;

        public LetterPrint(PrintDocument document) {
            preview = new PrintPreviewControl {
                Dock = DockStyle.Fill,
                Document = document,
                Zoom = 0.85
            };

            int x = (Screen.PrimaryScreen.Bounds.Width - CommandButton.DefaultWidth) / 2;
            close = new CommandButton(Resources.Close) {
                Location = new Point(x - CommandButton.DefaultWidth - 10, 0),
            };
            close.Click += (s, e) => {
                this.Close();
            };

            print = new CommandButton(Resources.Print) {
                Location = new Point(x + CommandButton.DefaultWidth + 10, 0),
            };
            print.Click += (s, e) => {
                document.Print();
            };

            panel = new Panel {
                Dock = DockStyle.Bottom,
                Height = CommandButton.DefaultHeight + 2,
            };
            panel.Controls.Add(close);
            panel.Controls.Add(print);

            this.Controls.Add(preview);
            this.Controls.Add(panel);
        }

        void OnPrintLetter(object sender, EventArgs e) {

        }
    }
}
