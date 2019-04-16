using SelfService.Code;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Keyboard : Form
    {
        const int LEFT = 10;
        const int TOP = 40;

        static bool shown;

        readonly Input myControl;
        readonly string[] rows = new string[6];
        readonly NumaricKeyboard numaricKeyboard;
        readonly TextBox box;

        bool arabic = true;

        public Keyboard(Input control = null) {
            arabic = true;

            int height = CharacterButton.DefaultHeight * 6;
            int width = Screen.PrimaryScreen.Bounds.Width; 
            int left = 0;// (Screen.PrimaryScreen.Bounds.Width - width) / 2;
            int top = Screen.PrimaryScreen.Bounds.Height - height;

            RightToLeftLayout = true;
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(left, top - 20);
            Size = new Size(width, height + 40);
            Padding = new Padding(10);
            Name = "Keyboard";
#if !DEBUG
            TopMost = true; 
#endif
            Visible = false;

            myControl = control;

            box = new TextBox {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Top,
                Height = CharacterButton.DefaultHeight,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                Font = myControl != null ? myControl.Font : new Font(Fonts.ALMohanad, 18),
                Margin = new Padding(0),
                TextAlign = HorizontalAlignment.Right,
                Text = myControl != null ? myControl.Text : "",
            };

            if (myControl.IsPassword) box.Text = "";

            rows[0] = @"ض ص ث ق ف غ ع ه خ ح ج د";
            rows[1] = @"ش س ي ب ل ا ت ن م ك ط ذ";
            rows[2] = @"$ ئ ء ؤ ر * ى ة و ز ظ @";

            rows[3] = @"  Q W E R T Y U I O P  ";
            rows[4] = @"A B C D E F G H I J K L";
            rows[5] = @"$   Z X C V B N M     @";

            int x = LEFT;
            int y = TOP + box.Height;
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
                            Size = new Size(CharacterButton.DefaultWidth, 68),
                        };
                        Controls.Add(label);
                    } else {
                        CharacterButton b = new CharacterButton(ar, en, x, y);
                        b.Click += OnCharacterKeyClick;
                        Controls.Add(b);
                    }
                    x += CharacterButton.XStep;
                }

                x = LEFT;
                y += CharacterButton.YStep;
            }

            Button sbacebar = new Button {
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(Fonts.ALMohanadBold, 25f),
                Location = new Point(LEFT, y),
                Size = new Size((CharacterButton.DefaultWidth * 12) + (110), CharacterButton.DefaultHeight - 5),
                Cursor = Cursors.Hand,
                TabStop = false,
                Text = " ",
                Tag = " ",
                UseVisualStyleBackColor = false,
            };
            sbacebar.Click += OnCharacterKeyClick;
            Controls.Add(sbacebar);

            x = (Screen.PrimaryScreen.Bounds.Right - (10 + NumaricKeyboard.DefaultWidth)) - CharacterButton.DefaultWidth;
            y = (TOP - 5) + box.Height;
            numaricKeyboard = new NumaricKeyboard(new Point(x, y), myControl, box);
            Controls.Add(numaricKeyboard);

            x = numaricKeyboard.Right + 0;
            y = numaricKeyboard.Top;
            Button close = new Button {
                Text = ('\u00CF').ToString(),
                Size = new Size(CharacterButton.DefaultWidth, CharacterButton.DefaultHeight),
                Location = new Point(x, y),
                BackColor = Color.Black,
                ForeColor = Color.WhiteSmoke,
                Font = new Font(Fonts.Wingdings2, 30f),
                Cursor = Cursors.Hand,
            };
            close.Click += OnCloseKeyClick;

            Controls.Add(box);
            Controls.Add(close);
        }

        void OnCharacterKeyClick(object s, EventArgs e) {
            if (myControl == null) {
                return;
            }

            string back = ('\u00D2').ToString();
            string lang = ('\u0037').ToString();
            string space = " ";
            //string text = (s as Control).Text;
            string tag = (s as Control).Tag.ToString();
            int max = myControl.MaxLength;
            bool isNumaric = myControl.IsNumaric;

            if (tag.Equals(back)) {
                try {
                    myControl.Text = myControl.Text.Substring(0, myControl.Text.Length - 1);
                    box.Text = myControl.Text;
                    box.PasswordChar = myControl.PasswordChar;
                    return;
                } catch (Exception) {
                    return;
                }
            }

            if (tag.Equals(space)) {
                myControl.Text += space;
                return;
            }

            if (tag.Equals(lang)) {
                arabic = !arabic;
                ChangeLayout();
                return;
            }

            char ch = tag[0];
            if (!Char.IsNumber(ch)) {
                if (isNumaric) {
                    return;
                }
            }

            
            var btn = (s as CharacterButton);
            myControl.Text += arabic ? btn.ArabicChar : btn.EnglishChar;
            if (!myControl.IsPassword) box.Text = myControl.Text;
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

        protected override void OnLoad(EventArgs e) {
            if (shown) {
                Close();
                return;
            }
            shown = true;
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            shown = false;
            base.OnFormClosing(e);
        }
    }
}
