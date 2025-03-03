using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Moq;

namespace Testes.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();

            _employeeService = new EmployeeService(_unitOfWorkMock.Object, _userServiceMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetEmployees_ShouldReturnEmployees()
        {
            // Criando lista fake de funcionários
            var fakeEmployees = new List<Employee>
            {
                new Employee { CodUsuario = 1, Nome = "Funcionario 1" },
                new Employee { CodUsuario = 2, Nome = "Funcionario 2" }
            };

            // Configurando mock para retornar a lista fake
            _unitOfWorkMock.Setup(u => u.EmployeeRepository.GetAllEmployeesAsync())
                           .ReturnsAsync(fakeEmployees);

            // Chamando o método
            var result = await _employeeService.GetEmployees();

            // Validações
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void PostEmployee_ShouldReturnEmployeeId()
        {
            // Criando request fake
            var request = new EmployeeRequest
            {
                Nome = "Novo Funcionário",
                Cep = "99999999",
                Telefone = "999999999",
                Celular = "999999999",
                DataNascimento = Convert.ToDateTime("1993-11-30"),
                Email = "teste@teste.com",
                NomeGestor = "Gestor",
                Sobrenome = "Sobrenome",
                User = new UserRequest
                {
                    Cpf = "99999999999",
                    Password = "password",
                    Roles = "ADM"
                }
            };

            // Criando Employee fake
            var fakeEmployee = new Employee { CodUsuario = 10, Nome = "Novo Funcionário" };

            // Configurando os mocks
            _userServiceMock.Setup(u => u.PostUser(request.User)).ReturnsAsync(10);
            _mapperMock.Setup(m => m.Map<Employee>(request)).Returns(fakeEmployee);
            _unitOfWorkMock.Setup(u => u.EmployeeRepository.PostEmployee(fakeEmployee)).Returns(1);

            // Chamando o método
            var result = _employeeService.PostEmployee(request);

            // Validação
            Assert.Equal(1, result);
        }

        [Fact]
        public void PutEmployee_ShouldReturnTrue()
        {
            // Criando request fake
            var request = new EmployeeRequest
            {
                Nome = "Novo Funcionário",
                Cep = "99999999",
                Telefone = "999999999",
                Celular = "999999999",
                DataNascimento = Convert.ToDateTime("1993-11-30"),
                Email = "teste@teste.com",
                NomeGestor = "Gestor",
                Sobrenome = "Sobrenome",
                User = new UserRequest
                {
                    Cpf = "99999999999",
                    Password = "password",
                    Roles = "ADM"
                }
            };

            // Criando Employee fake
            var fakeEmployee = new Employee { CodUsuario = 10, Nome = "Novo Funcionário" };

            // Configurando os mocks
            _mapperMock.Setup(m => m.Map<Employee>(request)).Returns(fakeEmployee);
            _unitOfWorkMock.Setup(u => u.EmployeeRepository.PutEmployee(fakeEmployee)).Returns(true);

            // Chamando o método
            var result = _employeeService.PutEmployee(request);

            // Validação
            Assert.True(result);
        }

    }
}
