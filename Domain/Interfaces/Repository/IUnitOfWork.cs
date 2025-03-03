namespace Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRolesRepository RolesRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
    }
}
