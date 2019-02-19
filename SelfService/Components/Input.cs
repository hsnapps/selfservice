using SelfService.Code;
using System;
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
            box.GotFocus += (s, e) => {
                InputGotFocus?.Invoke(this, e);
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

        public static int DefaultWidth = 420;
        public static int DefaultHeight = 120;

        public int MaxLength {
            get => box.MaxLength;
            set => box.MaxLength = value;
        }
        public Masks Mask { get; set; }

        public event InputGotFocusCallback InputGotFocus;

        public override string Text {
            get => box.Text;
            set => box.Text = value;
        }
    }
}
