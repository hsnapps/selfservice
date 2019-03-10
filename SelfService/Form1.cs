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
            string src = @"C:\D\code\khalid\SelfService\SelfService\Pdf\StudentGuide.pdf";
            axAcroPDF1.LoadFile(src);
        }

        void button3_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
