using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Keyboard : UserControl
    {
        Button[] buttons = new Button[12];
        string numbers = "123456789˂0˅";

        public Keyboard() {
            InitializeComponent();
        }

        int x = 3;
        int y = 3;

        void InitializeComponent() {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i] = new Button {
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Microsoft Sans Serif", 25f),
                    Location = new Point(x, y),
                    Name = String.Format("button{0}", i),
                    Size = new Size(77, 68),
                    TabIndex = 0,
                    Text = numbers[i].ToString(),
                    UseVisualStyleBackColor = false,
                };
                buttons[i].Click += OnKeyClick;

                x += 83;
                if (x % 3 == 0) {
                    x = 3;
                    y += 74;
                }
            }

            Controls.AddRange(buttons);

            Size = new Size((buttons[0].Width * 3) + (6 * 2), (buttons[0].Height * 3) + (6 * 2));
            Size = new Size(248, 300);
            x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            Location = new Point(x, y);
            this.Visible = false;
        }

        void OnKeyClick(object sender, EventArgs e) {
            if (Control == null) {
                this.Visible = false;
                return;
            }

            Button me = (Button)sender;
            switch (me.Text) {
                case "˂":
                    try {
                        Control.Text = Control.Text.Remove(Control.Text.Length - 1);
                    } catch (ArgumentOutOfRangeException) {
                        Control.Text = "";
                    }
                    break;

                case "˅":
                    this.Visible = false;
                    break;

                default:
                    int maxLength = 0;
                    if (Control is Input) {
                        maxLength = (Control as Input).MaxLength;
                    }
                    if (Control is DateInput) {
                        maxLength = (Control as DateInput).MaxLength;
                    }
                    if (Control.Text.Length < maxLength) {
                        Control.Text = Control.Text + (sender as Button).Text;
                    }
                    break;
            }
        }

        public Control Control { get; set; }
    }
}
