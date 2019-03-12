using Newtonsoft.Json.Linq;
using SelfService.Code;
using SelfService.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
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
            var parameters = new DBParameters(ConnectionType.MySQL, lines[1], lines[2], lines[3], lines[4], lines[5]);
            //var cs = "Data Source={0};Pooling=true;FailIfMissing=false;BinaryGUID=false;New=false;Compress=true;Version=3";
            CONNECTION_STRING = parameters.ConnectionString;
        }

        internal static List<string> ReadPlans() {
            List<string> data = new List<string>();

            try {
                string statement = "SELECT DISTINCT screen FROM `plans` WHERE ISNULL(`to_url`) AND !ISNULL(`screen`);";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            var value = reader[0].ToString();
                            data.Add(value);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return data;
        }

        internal static Dictionary<string, string> ReadPlanButtons(string screen = null) {
            Dictionary<string, string> data = new Dictionary<string, string>();

            try {
                string statement = "SELECT `button`, `to_screen`, `to_url` FROM `plans` WHERE `screen` = '" + screen + "';";
                if (String.IsNullOrEmpty(screen)) {
                    statement = "SELECT `button`, `to_screen`, `to_url` FROM `plans` WHERE ISNULL(`screen`);";
                }
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            var button = reader["button"].ToString();
                            var to_screen = reader["to_screen"].ToString();
                            var to_url = reader["to_url"].ToString();
                            if (String.IsNullOrEmpty(to_screen)) {
                                data.Add(button, to_url);
                            } else {
                                data.Add(button, to_screen);
                            }
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return data;
        }

        internal static void GetExamDuration(ref string start, ref string end) {
            try {
                string sql1 = "select value from settings where `category` = 'academic' and `key` = 'examStart';";
                string sql2 = "select value from settings where `category` = 'academic' and `key` = 'examEnd';";

                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql1, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            start = reader.GetString(0);
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(sql2, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            end = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {

            }
        }

        internal static string GetEmail(string config) {
            string email = "";

            try {
                string statement = "select value from settings where `category` = 'email' and `key` = '" + config + "';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            email = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
                
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
                    JToken t_url = c["url"];
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
            try {
                string sql1 = "select value from settings where `category` = 'manager' and `key` = 'title';";
                string sql2 = "select value from settings where `category` = 'manager' and `key` = 'name';";

                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql1, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            managerTitle = reader.GetString(0);
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(sql2, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            managerName = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
                
            }
        }

        internal static string GetVideoPath() {
            return Application.StartupPath + "\\Videos\\" + GetVideo("path");
        }

        internal static string GetVideoUrl() {
            return GetVideo("web");
        }

        static string GetVideo(string key) {
            string video = "";
            try {
                string statement = String.Format("select value from settings where `category` = 'video' and `key` = '{0}';", key);

                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            video = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return video;
        }

        internal static string GetVideoSelection() {
            string selection = "";

            try {
                string statement = "select value from settings where `category` = 'video' and `key` = 'select';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            selection = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
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
            try {
                string sql = String.Format(statement, trainee_num, id_number);

                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            student = new Student(reader);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {

            }

            return student;
        }

        internal static int GetTimeout() {
            int timeout = 60 * 2 * 1000;

            try {
                string statement = "select value from settings where `category` = 'config' and `key` = 'timeout';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            timeout = Convert.ToInt32(reader.GetString(0));
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return timeout;
        }

        internal static List<string> GetSubject(string category) {
            List<string> data = new List<string>();

            try {
                string statement = "SELECT value FROM settings WHERE category = 'subjects' AND `key` = '" + category + "';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            var value = reader["value"].ToString();
                            data.Add(value);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return data;
        }

        internal static Dictionary<string, string> GetValues(string category) {
            Dictionary<string, string> data = new Dictionary<string, string>();

            try {
                string statement = "select `key`, value from settings where `category` = '" + category + "';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            var value = reader["value"].ToString();
                            var key = reader["key"].ToString();
                            data.Add(key, value);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return data;
        }

        internal static DataTable GetCourses() {
            DataTable table = new DataTable("courses");

            try {
                table.Columns.AddRange(new DataColumn[] {
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
            });

                string statement = String.Format(Resources.CoursesSQL, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                    connection.Close();
                }
            } catch (Exception) {
            }

            return table;
        }

        internal static void Log(string key, string value) {
            string statement = String.Format("INSERT INTO logs (student_id, key, value) VALUES('{0}', '{1}', '{2}');", BaseForm.Student.ID, key, value);
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception) {

                    }
                }
            }
        }
    }
}
