using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Components
{
    class DateInput : UserControl
    {
        const string DATE_MASK = "00/00/0000";
        const int BOX_Y = 50;
        const int BOX_HEIGHT = 44;

        Label label, separator_1, separator_2;
        Panel panel;
        TextBox year, month, day;
        TextBox current;

        public DateInput(string caption, string mask) {
            InitializeComponent(caption, mask);
        }

        public DateInput() : this("", DateInput.DATE_MASK) { }
        public DateInput(string caption) : this(caption, DateInput.DATE_MASK) { }

        void InitializeComponent(string caption, string mask) {
            SuspendLayout();

            label = new Label {
                Dock = DockStyle.Top,
                AutoSize = false,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Name = "label1",
                Size = new Size(400, 40),
                TabStop = false,
                Text = caption,
                TextAlign = ContentAlignment.TopCenter,
            };
            separator_1 = new Label {
                AutoSize = true,
                Dock = DockStyle.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                BackColor = Color.Transparent,
                Name = "separator_1",
                TabStop = false,
                Text = "/",
                TextAlign = ContentAlignment.TopCenter,
            };
            separator_2 = new Label {
                AutoSize = true,
                Dock = DockStyle.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                BackColor = Color.Transparent,
                Name = "separator_2",
                TabStop = false,
                Text = "/",
                TextAlign = ContentAlignment.TopCenter,
            };
            year = new TextBox {
                Dock = DockStyle.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(0, BOX_Y),
                Name = "year",
                Size = new Size(180, BOX_HEIGHT),
                TabStop = false,
                MaxLength = 4,
                TextAlign = HorizontalAlignment.Center,
                Tag = 2030,
            };
            month = new TextBox {
                Dock = DockStyle.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(year.Width + 2, BOX_Y),
                Name = "month",
                Size = new Size(80, BOX_HEIGHT),
                TabStop = false,
                MaxLength = 2,
                TextAlign = HorizontalAlignment.Center,
                Tag = 12,
            };
            day = new TextBox {
                Dock = DockStyle.Left,
                Font = new Font("Microsoft Sans Serif", 28f),
                Location = new Point(month.Width + 2, BOX_Y),
                Name = "day",
                Size = new Size(80, BOX_HEIGHT),
                TabStop = false,
                MaxLength = 2,
                TextAlign = HorizontalAlignment.Center,
                Tag = 31,
            };

            year.GotFocus += OnInputGotFocus;
            month.GotFocus += OnInputGotFocus;
            day.GotFocus += OnInputGotFocus;

            year.TextChanged += OnTextChanged;
            month.TextChanged += OnTextChanged;
            day.TextChanged += OnTextChanged;

            panel = new Panel {
                Height = BOX_HEIGHT + 20,
                Dock = DockStyle.Bottom,
                BackColor = Color.Transparent,
            };
            panel.Controls.Add(day);
            panel.Controls.Add(separator_1);
            panel.Controls.Add(month);
            panel.Controls.Add(separator_2);
            panel.Controls.Add(year);

            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(label);
            Controls.Add(panel);
            Name = label.Text;
            Size = new Size(DefaultWidth, DefaultHeight);
            BackColor = Color.Transparent;
            ResumeLayout(false);
            PerformLayout();
        }

        void OnTextChanged(object sender, EventArgs e) {
            TextBox box = (sender as TextBox);
            if (box.Text.Length == 0) return;

            string tag = box.Tag.ToString();
            int max = Convert.ToInt32(tag);
            int value = Convert.ToInt32(box.Text);

            if (value > max) {
                this.Text = "";
            }
        }

        void OnInputGotFocus(object sender, EventArgs e) {
            current = (sender as TextBox);
            InputGotFocus?.Invoke(this, e);
        }

        public static int DefaultWidth = 420;
        public static int DefaultHeight = 130;

        public int MaxLength {
            get => current.MaxLength;
            set => current.MaxLength = value;
        }
        public int Year { get => String.IsNullOrEmpty(year.Text) ? 0 : Convert.ToInt32(year.Text); }
        public int Month { get => String.IsNullOrEmpty(month.Text) ? 0 : Convert.ToInt32(month.Text); }
        public int Day { get => String.IsNullOrEmpty(year.Text) ? 0 : Convert.ToInt32(day.Text); }

        public event InputGotFocusCallback InputGotFocus;

        public override string Text {
            get => current.Text;
            set => current.Text = value;
        }
        public string Date {
            set {
                string[] parts = value.Split(new char[] { '-', '/', '\\' });
                year.Text = parts[0];
                month.Text = parts[1];
                day.Text = parts[2];
            }
            get { return year.Text + separator_1.Text + month.Text + separator_2.Text + day.Text; }
        }
    }
}
