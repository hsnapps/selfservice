using System.Data.SQLite;

namespace SelfService.Models
{
    class Student
    {
        public Student(SQLiteDataReader reader) {
            ID = reader["id"].ToString();
            Email = reader["email"].ToString();
            Mobile = reader["mobile"].ToString();
            Name_EN = reader["name_en"].ToString();
            Name_AR = reader["name_ar"].ToString();
            ID_Number = reader["id_num"].ToString();
            Program = reader["program"].ToString();
            Section = reader["section"].ToString();
            Level = reader["level"].ToString();            
            Unit = reader["unit"].ToString();
            Term = reader["term"].ToString();
        }

        public Student(string id, string email, string mobile, string name_ar, string name_en, string id_number, string program, string section, string level, string unit, string term) {
            ID = id;
            Email = email;
            Mobile = mobile;
            Name_EN = name_en;
            Name_AR = name_ar;
            ID_Number = id_number;
            Program = program;
            Section = section;
            Level = level;
            Unit = unit;
            Term = term;
        }

        public string ID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Name_EN { get; set; }
        public string Name_AR { get; set; }
        public string ID_Number { get; set; }
        public string Program { get; set; }
        public string Section { get; set; }
        public string Level { get; set; }
        public string Unit { get; set; }
        public string Term { get; set; }
    }
}
