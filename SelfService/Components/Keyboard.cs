using SelfService.Code;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Keyboard : Form
    {
        const int LEFT = 10;
        const int TOP = 10;
        const int BTNWIDTH = 77;
        const int BTNHEIGHT = 68;
        const int X_STEP = 83;
        const int Y_STEP = 74;

        readonly Control control;
        readonly string[] rows = new string[3];
        readonly NumaricKeyboard numaricKeyboard;

        public Keyboard(Control control = null) {
            int height = BTNHEIGHT * 5;
            int width = Screen.PrimaryScreen.Bounds.Width; //BTNWIDTH * 13 + 20;
            int left = 0;// (Screen.PrimaryScreen.Bounds.Width - width) / 2;
            int top = Screen.PrimaryScreen.Bounds.Height - height;

            RightToLeftLayout = true;
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(left, top);
            Size = new Size(width, height);
            Padding = new Padding(10);
            Name = "Keyboard";
            TopMost = true;
            Visible = false;

            this.control = control;

            rows[0] = @"ض ص ث ق ف غ ع ه خ ح ج د";
            rows[1] = @"ش س ي ب ل ا ت ن م ك ط ذ";
            rows[2] = @"# ئ ء ؤ ر * ى ة و ز ظ #";    // 11

            int x = LEFT;
            int y = TOP;
            for (int i = 0; i < rows.Length; i++) {
                foreach (var c in rows[i]) {
                    if (c == ' ') continue;

                    if (c == '#') {
                        Label label = new Label {
                            AutoSize = false,
                            BackColor = Color.Transparent,
                            Location = new Point(x, y),
                            Size = new Size(BTNWIDTH, 68),
                        };
                        Controls.Add(label);
                    } else {
                        Button b = new Button {
                            BackColor = Color.White,
                            FlatStyle = FlatStyle.Flat,
                            Font = new Font(Fonts.ALMohanadBold, 25f),
                            Location = new Point(x, y),
                            Size = new Size(BTNWIDTH, 68),
                            Cursor = Cursors.Hand,
                            TabStop = false,
                            Text = c == '*' ? ('\uFEFB').ToString() : c.ToString(),
                            Tag = c == '*' ? "لا" : c.ToString(),
                            UseVisualStyleBackColor = false,
                        };
                        b.Click += DoSendKeys;
                        Controls.Add(b);
                    }
                    x += X_STEP;
                }

                x = LEFT;
                y += Y_STEP;
            }

            Button sbacebar = new Button {
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(Fonts.ALMohanadBold, 25f),
                Location = new Point(LEFT, y),
                Size = new Size(1000, BTNHEIGHT - 5),
                Cursor = Cursors.Hand,
                TabStop = false,
                Text = "",
                Tag = " ",
                UseVisualStyleBackColor = false,
            };
            sbacebar.Click += DoSendKeys;
            Controls.Add(sbacebar);

            x = (Screen.PrimaryScreen.Bounds.Right - (10 + NumaricKeyboard.DefaultWidth)) - BTNWIDTH;
            y = TOP - 5;
            numaricKeyboard = new NumaricKeyboard(new Point(x, y));
            Controls.Add(numaricKeyboard);

            x = numaricKeyboard.Right + 20;
            y = TOP - 5;
            Button close = new Button {
                Text = ('\u2196').ToString(),
                Size = new Size(BTNWIDTH, BTNHEIGHT),
                Location = new Point(x, y),
                BackColor = Color.Black,
                ForeColor = Color.WhiteSmoke,
            };
            close.Click += (s, e) => { Hide(); };
            Controls.Add(close);
        }

        void DoSendKeys(object s, EventArgs e) {
            string text = (s as Button).Text;
            SendKeys.SendWait(text);
        }
    }
}
