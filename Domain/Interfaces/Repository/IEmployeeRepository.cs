using Domain.Entities;
using Domain.Models.Response;

namespace Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository
    {
        public Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByCodUsuarioAsync(int codUsuario);
        public Task<EmployeeResponse> GetEmployeeAndUserByCodUsuarioAsync(int codUsuario);
        public int PostEmployee(Employee request);
        public bool PutEmployee(Employee request);
    }
}
