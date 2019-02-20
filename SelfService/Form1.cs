using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SelfService
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        void button2_Click(object sender, EventArgs e) {
            byte[] asciiBytes = Encoding.UTF8.GetBytes(textBox1.Text);
            foreach (byte b in asciiBytes) {
                listBox1.Items.Add(b);
            }
            textBox1.Text = "";
        }

        void button1_Click(object sender, EventArgs e) {
            string copy = "";
            foreach (var item in listBox1.Items) {
                copy += item.ToString() + ", ";
            }
            listBox1.Items.Clear();
            Clipboard.SetText(copy);
        }

        void button3_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
