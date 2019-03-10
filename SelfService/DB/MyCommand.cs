using System.Data;

namespace SelfService.DB
{
    class MyCommand : System.Data.IDbCommand
    {
        MyConnection _connection;
        readonly MySql.Data.MySqlClient.MySqlCommand mysql;
        readonly System.Data.SQLite.SQLiteCommand sqlite;

        IDbConnection IDbCommand.Connection { get => _connection; }
        IDbTransaction IDbCommand.Transaction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        string IDbCommand.CommandText { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        int IDbCommand.CommandTimeout { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        CommandType IDbCommand.CommandType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        IDataParameterCollection IDbCommand.Parameters => throw new System.NotImplementedException();

        UpdateRowSource IDbCommand.UpdatedRowSource { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        void IDbCommand.Prepare() {
            throw new System.NotImplementedException();
        }

        void IDbCommand.Cancel() {
            throw new System.NotImplementedException();
        }

        IDbDataParameter IDbCommand.CreateParameter() {
            throw new System.NotImplementedException();
        }

        int IDbCommand.ExecuteNonQuery() {
            throw new System.NotImplementedException();
        }

        IDataReader IDbCommand.ExecuteReader() {
            throw new System.NotImplementedException();
        }

        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior) {
            throw new System.NotImplementedException();
        }

        object IDbCommand.ExecuteScalar() {
            throw new System.NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MyCommand() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void System.IDisposable.Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
