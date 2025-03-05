using Domain.Entities;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeResponse>> GetEmployees();
        public int PostEmployee(EmployeeRequest request);
        public Task<bool> PutEmployee(EmployeeUpdateRequest request, int codUsuario);
        public Task<EmployeeResponse> GetEmployeeByCodUsuario(int codUsuario);
    }
}
