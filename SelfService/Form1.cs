﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
            comboBox1.DataSource = DB.Execute.GetConfig("maintainence");
        }
    }
}
