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

        private void StartDBUpdate(object sender, EventArgs e) {
            var path = Application.StartupPath + @"\DB\env.txt";
            string[] lines = File.ReadAllLines(path);
            var parameters = new DBParameters(ConnectionType.MySQL, lines[1], lines[2], lines[3], lines[4], lines[5]);
            var connectionString = parameters.ConnectionString+ "";

            using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                connection.Open();

                var sql = "SELECT DISTINCT button FROM plans;";
                List<string> buttons = new List<string>();
                using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        buttons.Add(reader[0].ToString());
                    }
                }

                progressBar1.Minimum = 0;
                progressBar1.Maximum = buttons.Count;
                progressBar1.Value = 0;

                var template = "UPDATE plans SET button_label = '{0}' WHERE button = '{1}'";
                
                foreach (var btn in buttons) {
                    var label = Tools.ReadPlanResource(btn);
                    var statement = String.Format(template, label, btn);

                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        command.ExecuteNonQuery();
                    }
                    progressBar1.Value++;
                }
                connection.Close();
            }
            MessageBox.Show("Done!");
        }
    }
}
