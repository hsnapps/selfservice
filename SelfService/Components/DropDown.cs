using SelfService.Code;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class DropDown : UserControl
    {
        Label label;
        ComboBox box;
        readonly List<string> data;

        public DropDown(string text, string category) {
            data = DB.Execute.GetSubject(category);
            InitializeComponent(text);
        }

        void InitializeComponent(string text) {
            label = new Label();
            box = new ComboBox();

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
            label.TextAlign = ContentAlignment.MiddleLeft;

            // box
            box.Dock = DockStyle.Fill;
            box.Font = new Font(Fonts.ALMohanad, 20f);
            box.Location = new Point(4, 4);
            box.Name = "box";
            box.Height = 50;
            box.TabIndex = 1;
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            box.DataSource = data;
            box.SelectedIndexChanged += (s, e) => {
                this.Value = (s as ComboBox).SelectedIndex.ToString();
            }; ;


            int width = Screen.PrimaryScreen.Bounds.Width - 100;

            // InlineInput
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(box);
            Controls.Add(label);
            Name = "InlineInput";
            Padding = new Padding(4);
            RightToLeft = RightToLeft.Yes;
            Size = new Size(width, 58);
        }

        public override string Text { get => box.Text; set => box.Text = value; }
        public string Value { get; private set; }
    }
}
