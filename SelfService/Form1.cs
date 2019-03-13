using MySql.Data.MySqlClient;
using SelfService.Code;
using SelfService.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SelfService
{
    public partial class Form1 : Form
    {
        delegate void UpdateProgressCallback(int value);
        delegate void SetProgressCallback(int max);

        public Form1() {
            CultureInfo culture = new CultureInfo("ar-SA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();
        }
    }
}
