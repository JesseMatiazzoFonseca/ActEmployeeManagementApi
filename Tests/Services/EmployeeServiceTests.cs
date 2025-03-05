using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Domain.Models.Response;
using Moq;
using static Domain.Models.Request.EmployeeUpdateRequest;

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
        public async Task GetEmployeeByCodUsuario_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            int codUsuario = 123;
            var expectedEmployee = new EmployeeResponse { CodUsuario = codUsuario, Nome = "Funcionário Teste" };

            _unitOfWorkMock.Setup(repo => repo.EmployeeRepository.GetEmployeeAndUserByCodUsuarioAsync(codUsuario))
                .ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetEmployeeByCodUsuario(codUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEmployee.CodUsuario, result.CodUsuario);
            Assert.Equal(expectedEmployee.Nome, result.Nome);
            _unitOfWorkMock.Verify(repo => repo.EmployeeRepository.GetEmployeeAndUserByCodUsuarioAsync(codUsuario), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeByCodUsuario_ShouldThrowException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int codUsuario = 999;
            _unitOfWorkMock.Setup(repo => repo.EmployeeRepository.GetEmployeeAndUserByCodUsuarioAsync(codUsuario))
                .ReturnsAsync((EmployeeResponse)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _employeeService.GetEmployeeByCodUsuario(codUsuario));
            Assert.Equal("Funcionário não encontrado", exception.Message);
            _unitOfWorkMock.Verify(repo => repo.EmployeeRepository.GetEmployeeAndUserByCodUsuarioAsync(codUsuario), Times.Once);
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnEmployees()
        {
            // Criando lista fake de funcionários
            var fakeEmployees = new List<EmployeeResponse>
            {
                new EmployeeResponse { CodUsuario = 1, Nome = "Funcionario 1" },
                new EmployeeResponse { CodUsuario = 2, Nome = "Funcionario 2" }
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
        public async Task PutEmployee_ShouldReturnTrue()
        {
            // Arrange - Criando request fake
            var request = new EmployeeUpdateRequest
            {
                Nome = "Novo Funcionário",
                Cep = "99999999",
                Telefone = "999999999",
                Celular = "999999999",
                DataNascimento = Convert.ToDateTime("1993-11-30"),
                Email = "teste@teste.com",
                NomeGestor = "Gestor",
                Sobrenome = "Sobrenome",
                User = new UserEmployeeResponse
                {
                    Cpf = "99999999999",
                    Roles = "ADM"
                }
            };

            // Criando Employee fake
            var fakeEmployee = new Employee { CodUsuario = 10, Nome = "Funcionário Existente" };
            var updatedEmployee = new Employee { CodUsuario = fakeEmployee.CodUsuario, Nome = request.Nome };

            // Configurando os mocks
            _unitOfWorkMock.Setup(u => u.EmployeeRepository.GetEmployeeByCodUsuarioAsync(fakeEmployee.CodUsuario))
                .ReturnsAsync(fakeEmployee); // Simula a busca pelo funcionário

            _mapperMock.Setup(m => m.Map<Employee>(request)).Returns(updatedEmployee);

            _unitOfWorkMock.Setup(u => u.EmployeeRepository.PutEmployee(updatedEmployee))
                .Returns(true); // Simula a atualização com sucesso

            var result = await _employeeService.PutEmployee(request, fakeEmployee.CodUsuario);
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.EmployeeRepository.GetEmployeeByCodUsuarioAsync(fakeEmployee.CodUsuario), Times.Once);
            _unitOfWorkMock.Verify(u => u.EmployeeRepository.PutEmployee(updatedEmployee), Times.Once);
        }


    }
}
