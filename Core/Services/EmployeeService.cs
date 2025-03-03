using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Models.Request;

namespace Domain.Interfaces.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IUserService _userService;
        public EmployeeService(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();
        }

        public int PostEmployee(EmployeeRequest request)
        {
            int codUsuario = Task.Run(() => _userService.PostUser(request.User)).Result;
            var employee = _mapper.Map<Employee>(request);
            employee.CodUsuario = codUsuario;
            var result = _unitOfWork.EmployeeRepository.PostEmployee(employee);
            return result;
        }
        public bool PutEmployee(EmployeeRequest request)
        {
            return _unitOfWork.EmployeeRepository.PutEmployee(_mapper.Map<Employee>(request));
        }
    }
}
