using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Components
{
    class NumaricKeyboard : UserControl
    {
        const int BTNWIDTH = 77;
        const int BTNHEIGHT = 68;

        Button[] buttons = new Button[12];
        readonly string numbers;

        public NumaricKeyboard() {
            numbers = "123456789˂0˅";
            InitializeComponent();
        }

        public NumaricKeyboard(Point location) {
            numbers = "123456789,0.";
            InitializeComponent();
            Location = location;
            Visible = true;
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
                    Size = new Size(BTNWIDTH, BTNHEIGHT),
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

            Size = new Size(NumaricKeyboard.DefaultWidth, NumaricKeyboard.DefaultHeight);
            Size = new Size(248, 300);
            x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            Location = new Point(x, y);
            Visible = false;
        }

        void OnKeyClick(object sender, EventArgs e) {
            Button me = (Button)sender;

            if (Control == null) {
                //this.Visible = false;
                DoSendKeys(me.Text);
                return;
            }
            
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

        void DoSendKeys(string text) {
            SendKeys.SendWait(text);
        }

        public Control Control { get; set; }
        public static int DefaultWidth { get { return (BTNWIDTH * 3) + (6 * 2); } }
        public static int DefaultHeight { get { return (BTNHEIGHT * 3) + (6 * 2); } }
    }
}
