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
        static readonly string CONNECTION_STRING = "";

        internal static string GetConfig(string key) {
            string value = "";

            try {
                string statement = "select value from settings where `category` = 'config' and `key` = '" + key + "';";
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            value = reader.GetString(0);
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw; 
#endif
            }

            return value;
        }

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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
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
#if DEBUG
                throw;
#endif
            }

            return data;
        }

        internal static DataTable GetRestCourses() {
            DataTable table = new DataTable("courses");

            try {
                table.Columns.AddRange(new DataColumn[] {
                    new DataColumn("course_code", typeof(String)),
                    new DataColumn("title", typeof(String)),
                    new DataColumn("approved_units_completed", typeof(Int32)),
                    new DataColumn("approved_units_required", typeof(String)),
                    new DataColumn("courses_completed", typeof(String)),
                    new DataColumn("courses_required", typeof(Single)),
                    new DataColumn("approved_units_required_for_program", typeof(Single)),
                    new DataColumn("finshed_hours", typeof(Int32)),
                });
                string sql = @"
SELECT r.course_code, c.title, r.approved_units_completed, r.approved_units_required,
r.courses_completed, r.courses_required, r.approved_units_required_for_program
FROM `tvtc`.`rest_courses` r
LEFT JOIN `tvtc`.`course_titles` c ON r.course_code = c.`code2`
WHERE r.student_id = '{0}';";
                string statement = String.Format(sql, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }

            return table;
        }

        internal static DataTable GetSchedule() {
            DataTable table = new DataTable("schedules");

            try {
                table.Columns.AddRange(new DataColumn[] {
                    new DataColumn("course_symbol", typeof(String)),
                    new DataColumn("course_name", typeof(String)),
                    new DataColumn("supervisor_name", typeof(String)),
                });

                string sql = @"
SELECT DISTINCT
`course_symbol`,
`course_name`,
`supervisor_name`
FROM schedules WHERE student_id = '{0}'";
                string statement = String.Format(sql, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }

            return table;
        }

        internal static DataTable GetDailySchedule() {
            DataTable table = new DataTable("daily_schedules");
            string sql = @"
SELECT * FROM `groups` g 
WHERE g.computer_num IN (
	SELECT s.teacher_id 
	FROM student_records s 
	WHERE s.std_no = '{0}'
)
AND g.reference_num IN (
	SELECT s.section_code 
	FROM student_records s 
	WHERE s.std_no = '{1}' 
);";
            string statement = String.Format(sql, BaseForm.Student.ID, BaseForm.Student.ID);
            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("course_code", typeof(String)),
                new DataColumn("course_title", typeof(String)),
                new DataColumn("group_type", typeof(String)),
                new DataColumn("days", typeof(String)),
                new DataColumn("times", typeof(String)),
                new DataColumn("hall", typeof(String)),
                new DataColumn("trainer", typeof(String)),
            });

            try {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw; 
#endif
            }
            return table;
        }

        internal static DataTable GetExamSchedule() {
            DataTable table = new DataTable("daily_schedules");
            string sql = @"
SELECT * FROM `exam_schedules` e 
WHERE e.teacher_id IN (
	SELECT s.teacher_id 
	FROM student_records s 
	WHERE s.std_no = '{0}'
)
AND e.`group` IN (
	SELECT s.section_code 
	FROM student_records s 
	WHERE s.std_no = '{1}' 
);";
            string statement = String.Format(sql, BaseForm.Student.ID, BaseForm.Student.ID);
            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("code", typeof(String)),
                new DataColumn("title", typeof(String)),
                new DataColumn("group", typeof(String)),
                new DataColumn("exa_date", typeof(String)),
                new DataColumn("exam_time", typeof(String)),
                new DataColumn("time", typeof(String)),
                new DataColumn("place1", typeof(String)),
            });

            try {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }
            return table;
        }

        internal static void Log(string key, string value) {
            string statement = String.Format("INSERT INTO `logs` (`student_id`, `key`, `value`) VALUES('{0}', '{1}', '{2}');", BaseForm.Student.ID, key, value);
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception) {
#if DEBUG
                        throw;
#endif
                    }
                }
                connection.Close();
            }
        }

        internal static void RemoveOldCopies() {
            try {
                string sql = @"DELETE FROM print_restrictions WHERE student_id = '{0}' AND print_date != CURDATE();";
                string statement = String.Format(sql, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }
        }

        internal static int CurrentCopy() {
            bool add = !CheckIfStudentExistsInPrintRestriction();
            if (add) {
                AddStudentIdToPrintRestrictions();
            }

            RemoveOldCopies();

            int copies = 0;

            try {
                string sql = @"SELECT copies FROM print_restrictions p WHERE p.student_id = '{0}' AND print_date = CURDATE();";
                string statement = String.Format(sql, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            copies = Convert.ToInt32(reader.GetString(0));
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw; 
#endif
            }

            return copies;
        }

        internal static bool CheckIfStudentExistsInPrintRestriction() {
            int found = 0;

            try {
                string statement = String.Format("SELECT COUNT(*) FROM print_restrictions p WHERE p.student_id = '{0}';", BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read()) {
                            found = Convert.ToInt32(reader.GetString(0));
                        }
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }

            return found > 0;
        }

        internal static void AddStudentIdToPrintRestrictions() {
            string statement = String.Format("INSERT INTO print_restrictions (student_id, print_date) VALUES('{0}', CURDATE());", BaseForm.Student.ID);
            try {
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }
        }

        internal static void IncreaseCopies() {
            try {
                int copies = CurrentCopy() + 1;

                string statement = String.Format("UPDATE print_restrictions SET copies = {0} WHERE student_id = '{1}';", copies, BaseForm.Student.ID);
                using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(statement, connection)) {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            } catch (Exception) {
#if DEBUG
                throw;
#endif
            }
        }

        internal static int GetMaxCopies() {
            int max = 2;

            string _max = GetConfig("max_print");
            if (!String.IsNullOrEmpty(_max)) {
                Int32.TryParse(_max, out max);
            }

            return max;
        }       
    }
}
