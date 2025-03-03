using Domain.Entities;
using Domain.Models.Request;

namespace Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public int PostEmployee(EmployeeRequest request);
        public bool PutEmployee(EmployeeRequest request);
    }
}
