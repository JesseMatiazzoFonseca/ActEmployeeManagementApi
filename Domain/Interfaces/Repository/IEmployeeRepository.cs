using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByCodUsuarioAsync(int codUsuario);
        public int PostEmployee(Employee request);
        public bool PutEmployee(Employee request);
    }
}
