using Newtonsoft.Json.Linq;
using SelfService.Code;
using SelfService.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using SelfService.Screens;
using System.Data;
using SelfService.Properties;

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

        internal static string GetYoutubeUrl() {
            string id = GetVideo("youtube");
            string url = "https://ytgrabber.p.rapidapi.com/app/get/" + id;
            YoutubeRespone youtube;

            using (HttpClient client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "DGN0v3Cft1mshsk7mpUF91dmRTOTp19kt6cjsn9cDqrtU4GFLS");
                var response = client.GetStringAsync(new Uri(url)).Result;
                //youtube = JsonConvert.DeserializeObject<YoutubeRespone>(response);
                youtube = new YoutubeRespone();
                JObject obj = JObject.Parse(response);
                youtube.Error = (string)obj["error"];
                youtube.ThumbnailUrl = (string)obj["thumbnailUrl"];
                youtube.Title = (string)obj["title"];
                JArray links = (JArray)obj["link"];

                youtube.Links = links.Select(c => {
                    //JToken t_link = c["link"];
                    JToken t_type = c["type"];
                    JToken t_format = t_type["format"];
                    JToken t_quality = t_type["quality"];
                    JToken t_url= c["url"];
                    YoutubeVideoType type =
                    new YoutubeVideoType((string)t_format, (string)t_quality);
                    return new Link(type, (string)t_url);
                }).ToList();
            }

            if (youtube == null) {
                return "";
            }

            if (youtube.Links == null) {
                return "";
            }

            if (!String.IsNullOrEmpty(youtube.Error)) {
                return "";
            }

            var mp4 = youtube.Links.FindAll(v => v.Type.Format == "mp4");
            foreach (var link in mp4) {
                if (link.Type.Quality.Contains("1080")) {
                    return link.URL;
                } else if (link.Type.Quality.Contains("720")) {
                    return link.URL;
                } else if (link.Type.Quality.Contains("360")) {
                    return link.URL;
                }
            }

            return "";
        }

        internal static void GetManager(ref string managerTitle, ref string managerName) {
            string sql1 = "select value from settings where category = 'manager' and key = 'title';";
            string sql2 = "select value from settings where category = 'manager' and key = 'name';";

            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql1, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        managerTitle = reader.GetString(0);
                    }
                }
                using (SQLiteCommand command = new SQLiteCommand(sql2, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        managerName = reader.GetString(0);
                    }
                }
                connection.Close();
            }
        }

        internal static string GetVideoPath() {
            return GetVideo("path");
        }

        internal static string GetVideoUrl() {
            return GetVideo("web");
        }

        static string GetVideo(string key) {
            string video = "";
            string statement = String.Format("select value from settings where category = 'video' and key = '{0}';", key);

            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        video = reader.GetString(0);
                    }
                }
                connection.Close();
            }

            return video;
        }

        internal static string GetVideoSelection() {
            string selection = "";

            string statement = "select value from settings where category = 'video' and key = 'select';";
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        selection = reader.GetString(0);
                    }
                }
                connection.Close();
            }

            return selection;
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

        internal static List<string> GetSubject(string category) {
            List<string> data = new List<string>();

            string statement = "SELECT value FROM settings WHERE category = 'subjects' AND key = '" + category + "';";
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

        internal static Dictionary<string,string> GetValues(string category) {
            Dictionary<string, string> data = new Dictionary<string, string>();

            string statement = "select key, value from settings where category = '" + category + "';";
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        var value = reader["value"].ToString();
                        var key = reader["key"].ToString();
                        data.Add(key, value);
                    }
                }
                connection.Close();
            }

            return data;
        }

        internal static DataTable GetCourses() {
            DataTable table = new DataTable("courses");
            table.Columns.AddRange(new DataColumn[] {
                //new DataColumn("id", typeof(Int32)),
                new DataColumn("registered", typeof(String)),
                new DataColumn("completed", typeof(String)),
                new DataColumn("authorized_units", typeof(Int32)),
                new DataColumn("course_name", typeof(String)),
                new DataColumn("course_symbol", typeof(String)),
                new DataColumn("gpa", typeof(Single)),
                new DataColumn("passed_units", typeof(Single)),
                new DataColumn("required_units", typeof(Int32)),
                new DataColumn("passed_subjects", typeof(Int32)),
                new DataColumn("required_subjects", typeof(Int32)),
                new DataColumn("level_name", typeof(String)),
                new DataColumn("level_id", typeof(String)),
                new DataColumn("prog_gpa", typeof(Single)),
                new DataColumn("accepted_prg_units", typeof(Int32)),
                new DataColumn("required_prg_units", typeof(Int32)),
                new DataColumn("accepted_prg_subjects", typeof(Int32)),
                new DataColumn("required_prg_subjects", typeof(Int32)),
                //new DataColumn("program", typeof(String)),
                //new DataColumn("specialization", typeof(String)),
                //new DataColumn("section", typeof(String)),
                //new DataColumn("level", typeof(String)),
                //new DataColumn("faculty", typeof(String)),
                //new DataColumn("student", typeof(String)),
                //new DataColumn("student_id", typeof(String)),
            });

            string statement = String.Format(Resources.CoursesSQL, BaseForm.Student.ID);
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(table);
                }
                connection.Close();
            }

            return table;
        }

        internal static void Log(string key, string value) {
            string template = "INSERT INTO logs (student_id, key, value, created_at, updated_at) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');";
            string statement = String.Format(template, BaseForm.Student.ID, key, value, DateTime.Now, DateTime.Now);
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(statement, connection)) {
                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception) {
                        throw;
                    }
                }
                connection.Close();
            }
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

    public static class LogValues
    {
        public static string Login = "login";
        public static string InvalidLogin = "login.invalid";
        public static string LettersGlobal = "letters.global";
        public static string LettersSCE = "letters.sce";
        public static string LettersExam = "letters.examination";
    }
}
