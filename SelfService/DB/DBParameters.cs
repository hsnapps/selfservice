using System;
using System.IO;
using System.Windows.Forms;

namespace SelfService.DB
{
    public enum ConnectionType
    {
        MySQL,
        SQLite3
    }

    public class DBParameters
    {
        public DBParameters(ConnectionType connectionType, string host, string port, string database, string username, string password) {
            var args = Environment.GetCommandLineArgs();
            foreach (var arg in args) {
                if (arg.StartsWith("--cn=")) {
                    var type = arg.Replace("--cn=", String.Empty);
                    ConnectionType = type == "sqlite" ? ConnectionType.SQLite3 : ConnectionType.MySQL;
                } else if (arg.StartsWith("--db=")) {
                    Database = arg.Replace("--db=", String.Empty);
                } else if (arg.StartsWith("--host=")) {
                    Host = arg.Replace("--host=", String.Empty);
                } else if (arg.StartsWith("--port=")) {
                    Port = arg.Replace("--port=", String.Empty);
                } else if (arg.StartsWith("--uesr=")) {
                    Username = arg.Replace("--uesr=", String.Empty);
                } else if (arg.StartsWith("--password=")) {
                    Password = arg.Replace("--password=", String.Empty);
                } else {
                    break;
                }
            }

            if (String.IsNullOrEmpty(Database)) {
                var path = Application.StartupPath + "\\DB\\database.sqlite";
                if (File.Exists(path)) {
                    Database = path;
                } else {
                    Database = "tvtc";
                }
            }

            ConnectionType = connectionType;
            if (String.IsNullOrEmpty(Database)) Database = database;
            if (String.IsNullOrEmpty(Host)) Host = host;
            if (String.IsNullOrEmpty(Port)) Port = port;
            if (String.IsNullOrEmpty(Username)) Username = username;
            if (String.IsNullOrEmpty(Password)) Password = password;

            switch (ConnectionType) {
                case ConnectionType.SQLite3:
                    ConnectionString = String.Format("Data Source={0};Pooling=true;FailIfMissing=false;BinaryGUID=false;New=false;Compress=true;Version=3", Database);
                    break;

                default:
                    ConnectionString = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", Host, Port, Database, Username, Password);
                    break;
            }
        }

        public DBParameters(ConnectionType type, params string[] args) : this(type, args[0], args[1], args[2], args[3], args[4]) {}

        public ConnectionType ConnectionType { get; private set; }
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string Database { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ConnectionString { get; set; }
    }
}
