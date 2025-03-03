using Domain.Interfaces.Repository;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private bool _disposed = false;

        public IEmployeeRepository EmployeeRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRolesRepository RolesRepository { get; }

        public UnitOfWork(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection ?? throw new ArgumentException(nameof(sqlConnection));
            _sqlConnection?.Open();

            EmployeeRepository = new EmployeeRepository(_sqlConnection);
            UserRepository = new UserRepository(_sqlConnection);
            RolesRepository = new RolesRepository(_sqlConnection);
        }
        public void BeginTransaction()
        {
            if (_sqlTransaction == null)
            {
                if (_sqlConnection?.State != ConnectionState.Open)
                    _sqlConnection.Open();

                _sqlTransaction = _sqlConnection.BeginTransaction();

                EmployeeRepository.SqlTransaction = _sqlTransaction;
                UserRepository.SqlTransaction = _sqlTransaction;
                RolesRepository.SqlTransaction = _sqlTransaction;
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
