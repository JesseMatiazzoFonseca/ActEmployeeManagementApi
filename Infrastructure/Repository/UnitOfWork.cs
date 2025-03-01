using Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private bool _disposed = false;
        public UnitOfWork(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ?? throw new ArgumentException(nameof(sqlConnection));
            _sqlConnection?.Open();
        }
        public void BeginTransaction()
        {
            if (_sqlTransaction == null)
            {
                if (_sqlConnection?.State != ConnectionState.Open)
                    _sqlConnection.Open();

                _sqlTransaction = _sqlConnection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_sqlTransaction == null)
                throw new InvalidEnumArgumentException("Transação não startada");

            _sqlTransaction?.Commit();
            _sqlTransaction.Dispose();
            _sqlTransaction = null;
        }
        public void Rollback()
        {
            if (_sqlTransaction == null)
                throw new InvalidEnumArgumentException("Transação não startada");
            _sqlTransaction.Rollback();
            _sqlTransaction.Dispose();
            _sqlTransaction = null;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sqlTransaction?.Dispose();
                    _sqlConnection?.Close();
                    _sqlConnection?.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
