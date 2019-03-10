using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SQLite;

namespace SelfService.DB
{
    class MyConnection : System.Data.IDbConnection
    {
        SQLiteConnection sqlite;
        MySqlConnection mysql;

        DBParameters _parameters;
        string _connectionString;

        public MyConnection(DBParameters parameters) {
            _parameters = parameters;

            switch (_parameters.ConnectionType) {
                case ConnectionType.MySQL:
                    mysql = new MySqlConnection();
                    break;
                case ConnectionType.SQLite3:
                    sqlite = new SQLiteConnection();
                    break;
            }

            ConnectionType = _parameters.ConnectionType;
        }

        public string ConnectionString {
            get => _parameters.ConnectionString;
            set => _parameters.ConnectionString = value;
        }

        public int ConnectionTimeout {
            get {
                return _parameters.ConnectionType == ConnectionType.SQLite3 ? sqlite.ConnectionTimeout : mysql.ConnectionTimeout;
            }
        }

        public string Database {
            get {
                return _parameters.ConnectionType == ConnectionType.SQLite3 ? sqlite.Database : mysql.Database;
            }
        }

        public ConnectionState State {
            get {
                return _parameters.ConnectionType == ConnectionType.SQLite3 ? sqlite.State : mysql.State;
            }
        }

        public IDbTransaction BeginTransaction() {
            if (_parameters.ConnectionType == ConnectionType.SQLite3) {
                return sqlite.BeginTransaction();
            }

            return mysql.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il) {
            if (_parameters.ConnectionType == ConnectionType.SQLite3) {
                return sqlite.BeginTransaction(il);
            }

            return mysql.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName) {
            switch (_parameters.ConnectionType) {
                case ConnectionType.MySQL:
                    mysql.ChangeDatabase(databaseName);
                    break;
                case ConnectionType.SQLite3:
                    sqlite.ChangeDatabase(databaseName);
                    break;
            }
        }

        public void Close() {
            switch (_parameters.ConnectionType) {
                case ConnectionType.MySQL:
                    mysql.Close();
                    break;
                case ConnectionType.SQLite3:
                    sqlite.Close();
                    break;
            }
        }

        public IDbCommand CreateCommand() {
            if (_parameters.ConnectionType == ConnectionType.SQLite3) {
                return sqlite.CreateCommand();
            }

            return mysql.CreateCommand();
        }

        public void Dispose() {
            switch (_parameters.ConnectionType) {
                case ConnectionType.MySQL:
                    mysql.Dispose();
                    break;
                case ConnectionType.SQLite3:
                    sqlite.Dispose();
                    break;
            }
        }

        public void Open() {
            switch (_parameters.ConnectionType) {
                case ConnectionType.MySQL:
                    mysql.Open();
                    break;
                case ConnectionType.SQLite3:
                    sqlite.Open();
                    break;
            }
        }

        public ConnectionType ConnectionType { get; private set; }
    }
}
