using SelfService.Code;
using SelfService.Screens;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Input : UserControl
    {
        private Label label;
        private TextBox box;

        public Input(string caption, bool password) {
            InitializeComponent(caption, password);
            IsPassword = password;
        }

        public Input(string caption, bool inline, bool multiline = false) {
            InitializeInlineInput(caption, multiline);
        }

        public Input() : this("", false) { }
        public Input(bool password) : this("", password) { }
        public Input(string caption) : this(caption, false) { }

        void InitializeComponent(string caption, bool password) {
            Mask = Masks.None;

            SuspendLayout();

            // label
            label = new Label {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Name = "label1",
                Size = new Size(399, 46),
                TabStop = false,
                Text = caption,
                TextAlign = ContentAlignment.TopCenter,
            };

            // box
            box = new TextBox {
                Dock = DockStyle.Bottom,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(0, 49),
                Name = "textBox1",
                Size = new Size(399, 44),
                TabStop = false,
                MaxLength = 10,
                PasswordChar = password ? '*' : '\0',
                TextAlign = HorizontalAlignment.Center,
            };
            box.Click += (s, e) => {
                //InputGotFocus?.Invoke(this, e);
                try {
                    ShowKeyboard();
                } catch (Exception) {
                    
                }
            };

            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(box);
            Controls.Add(label);
            Name = label.Text;
            Size = new Size(DefaultWidth, DefaultHeight);
            BackColor = Color.White;
            ResumeLayout(false);
            PerformLayout();
        }

        void InitializeInlineInput(string text, bool multiline = false) {
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
                ShowKeyboard();
            };
        }

        void ShowKeyboard() {
            BaseForm form = null;

            if (this.Parent is BaseForm) {
                form = (BaseForm)this.Parent;
            } else if (this.Parent is FlowLayoutPanel) {
                FlowLayoutPanel panel = (FlowLayoutPanel)this.Parent;
                form = (BaseForm)panel.Parent;
            } else if (this.Parent is Panel) {
                Panel panel = (Panel)this.Parent;
                form = (BaseForm)panel.Parent;
            }

            form.Keyboard.ChangeControl(this);
        }

        public static int DefaultWidth = 420;
        public static int DefaultHeight = 120;

        public int MaxLength {
            get => box.MaxLength;
            set => box.MaxLength = value;
        }
        public Masks Mask { get; set; }
        public bool IsNumaric { get; set; }

        //public event InputGotFocusCallback InputGotFocus;

        public override string Text {
            get => box.Text;
            set => box.Text = value;
        }
        public char PasswordChar {
            get => box.PasswordChar;
        }
        public bool IsPassword { get; internal set; }
    }
}
