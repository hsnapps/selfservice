using SelfService.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace SelfService.DB
{
    static class Execute
    {
        static string CONNECTION_STRING = "";

        static Execute() {
            var path = Application.StartupPath + @"\DB\env.txt";
            string[] lines = File.ReadAllLines(path);
            var parameters = new Parameters(lines[0], lines[1], lines[2], lines[3], lines[4], lines[5]);
            var cs = "Data Source={0};Pooling=true;FailIfMissing=false;BinaryGUID=false;New=false;Compress=true;Version=3";
            CONNECTION_STRING = String.Format(cs, parameters.Database);
        }

        internal static string GetEmail(string config) {
            string email = "";

            string statement = "select value from settings where category = 'email' and key = '" + config + "';";
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        email = reader.GetString(0);
                    }
                }
                connection.Close();
            }

            return email;
        }

        internal static Student Login(string trainee_num, string id_number) {
            string statement = @"
select id, email, mobile, name_ar, name_en, id_num, program, section, level, unit, term 
from students 
where id = '{0}' 
and id_num = '{1}';";
            Student student = null;
            string sql = String.Format(statement, trainee_num, id_number);

            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        student = new Student(reader);
                    }
                }
                connection.Close();
            }

            return student;
        }

        internal static int GetTimeout() {
            int timeout = 60 * 2 * 1000;

            string statement = "select value from settings where category = 'config' and key = 'timeout';";
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        timeout = Convert.ToInt32(reader.GetString(0));
                    }
                }
                connection.Close();
            }

            return timeout;
        }

        internal static List<string> GetConfig(string category) {
            List<string> data = new List<string>();

            string statement = "select value from settings where category = '" + category + "';";
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        var value = reader["value"].ToString();
                        data.Add(value);
                    }
                }
                connection.Close();
            }

            return data;
        }
    }

    public class Parameters
    {
        public Parameters(string connection, string host, string port, string database, string username, string password) {
            Connection = connection;
            Host = host;
            Port = port;
            Database = database;
            Username = username;
            Password = password;
        }

        public Parameters(params string[] args) : this(args[0], args[1], args[2], args[3], args[4], args[5]) {

        }

        public string Connection { get; private set; }
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string Database { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
