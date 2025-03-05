using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models.Request;
using Domain.Models.Response;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IUserService _userService;
        public EmployeeService(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetEmployees()
        {
            var result = await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();
            return result.Where(x => x.Roles != "ADM");
        }

        public async Task<EmployeeResponse> GetEmployeeByCodUsuario(int codUsuario)
        {
            return await _unitOfWork.EmployeeRepository.GetEmployeeAndUserByCodUsuarioAsync(codUsuario) ?? throw new Exception("Funcionário não encontrado");
        }

        public int PostEmployee(EmployeeRequest request)
        {
            int codUsuario = Task.Run(() => _userService.PostUser(request.User)).Result;
            var employee = _mapper.Map<Employee>(request);
            employee.CodUsuario = codUsuario;
            var result = _unitOfWork.EmployeeRepository.PostEmployee(employee);
            return result;
        }
        public async Task<bool> PutEmployee(EmployeeUpdateRequest request, int codUsuario)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByCodUsuarioAsync(codUsuario) ?? throw new Exception("Funcionário não encontrado");
            var model = _mapper.Map<Employee>(request);
            model.CodFuncionario = employee.CodFuncionario;
            model.CodUsuario = employee.CodUsuario;
            return _unitOfWork.EmployeeRepository.PutEmployee(model);
        }
    }
}
