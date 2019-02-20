using SelfService.Code;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SelfService.Components
{
    class InlineInput : UserControl
    {
        Label label;
        TextBox box;

        public InlineInput(string text, bool multiline = false) {
            InitializeComponent(text, multiline);
        }

        void InitializeComponent(string text, bool multiline = false) {
            label = new Label();
            box = new TextBox();

            int width = Screen.PrimaryScreen.Bounds.Width - 100;
            int height = multiline ? 250 : 58;

            // InlineInput
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(box);
            Controls.Add(label);
            Name = "InlineInput";
            Padding = new Padding(4);
            RightToLeft = RightToLeft.Yes;
            Size = new Size(width, height);

            // label
            label.Dock = DockStyle.Right;
            label.Font = new Font(Fonts.ALMohanadBold, 20f);
            label.Location = new Point(501, 4);
            label.Name = "label";
            label.Padding = new Padding(3);
            label.RightToLeft = RightToLeft.Yes;
            label.Size = new Size(200, 50);
            label.TabIndex = 0;
            label.Text = text;
            label.TextAlign = multiline ? ContentAlignment.TopLeft : ContentAlignment.MiddleLeft;

            // box
            box.Dock = DockStyle.Fill;
            box.Font = new Font("AL-Mohanad", 20f);
            box.Location = new Point(4, 4);
            box.Name = "box";
            box.Multiline = multiline;
            box.Height = multiline ? 100 : 50;

            box.Click += (s, e) => {
                string osk = Environment.SystemDirectory + "osk.exe";
                if (File.Exists(osk)) {
                    Process.Start(osk);
                }
            };
        }

        public override string Text { get => box.Text; set => box.Text = value; }
        public int MaxLength { get => box.MaxLength; set => box.MaxLength = value; }
    }
}
