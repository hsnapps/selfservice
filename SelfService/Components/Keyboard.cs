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

        readonly Control myControl;
        readonly string[] rows = new string[6];
        readonly NumaricKeyboard numaricKeyboard;

        bool arabic = true;

        public Keyboard(Control control = null) {
            arabic = true;

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

            myControl = control;

            rows[0] = @"ض ص ث ق ف غ ع ه خ ح ج د";
            rows[1] = @"ش س ي ب ل ا ت ن م ك ط ذ";
            rows[2] = @"$ ئ ء ؤ ر * ى ة و ز ظ @";

            rows[3] = @"  Q W E R T Y U I O P  ";
            rows[4] = @"A B C D E F G H I J K L";
            rows[5] = @"$   Z X C V B N M     @";

            int x = LEFT;
            int y = TOP;
            for (int i = 0; i < 3; i++) {
                for (int k = 0; k < 23; k++) {
                    var ar = rows[i][k];
                    var en = rows[i + 3][k];

                    if (ar == ' ') continue;

                    if (ar == '#') {
                        Label label = new Label {
                            AutoSize = false,
                            BackColor = Color.Transparent,
                            Location = new Point(x, y),
                            Size = new Size(BTNWIDTH, 68),
                        };
                        Controls.Add(label);
                    } else {
                        CharacterButton b = new CharacterButton(ar, en, x, y);                        
                        b.Click += OnCharacterKeyClick;
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
                Text = " ",
                Tag = " ",
                UseVisualStyleBackColor = false,
            };
            sbacebar.Click += OnCharacterKeyClick;
            Controls.Add(sbacebar);

            x = (Screen.PrimaryScreen.Bounds.Right - (10 + NumaricKeyboard.DefaultWidth)) - BTNWIDTH;
            y = TOP - 5;
            numaricKeyboard = new NumaricKeyboard(new Point(x, y), myControl);
            Controls.Add(numaricKeyboard);

            x = numaricKeyboard.Right + 20;
            y = TOP - 5;
            Button close = new Button {
                Text = ('\u00CF').ToString(),
                Size = new Size(BTNWIDTH, BTNHEIGHT),
                Location = new Point(x, y),
                BackColor = Color.Black,
                ForeColor = Color.WhiteSmoke,
                Font = new Font(Fonts.Wingdings2, 30f),
                Cursor = Cursors.Hand,
            };
            close.Click += OnCloseKeyClick;
            Controls.Add(close);
        }

        void OnCharacterKeyClick(object s, EventArgs e) {
            string back = ('\u00D2').ToString();
            string lang = ('\u0037').ToString();
            string text = (s as Control).Text;
            int max = (s as Input).MaxLength;
            bool isNumaric = (s as Input).IsNumaric;

            if (text.Equals(back)) {
                myControl.Text = myControl.Text.Substring(0, myControl.Text.Length - 1);
                return;
            }

            if (text.Equals(lang)) {
                arabic = !arabic;
                ChangeLayout();
                return;
            }

            if (s is Button) {
                myControl.Text += text;
                return;
            }

            if (myControl != null) {
                char ch = text[0];
                if (!Char.IsNumber(ch)) {
                    if (isNumaric) {
                        return;
                    }
                }
                myControl.Text += arabic ? (s as CharacterButton).ArabicChar : (s as CharacterButton).EnglishChar;
            }
        }

        void ChangeLayout() {
            //for (int i = 0; i < 3; i++) {}
            foreach (var c in this.Controls) {
                if (c is CharacterButton) {
                    CharacterButton button = (CharacterButton)c;
                    if (button.Ignore) {
                        continue;
                    }
                    button.Text = arabic ? button.ArabicChar : button.EnglishChar;
                }
            }
        }

        void OnCloseKeyClick(object s, EventArgs e) {
            Close();
        }
    }
}
