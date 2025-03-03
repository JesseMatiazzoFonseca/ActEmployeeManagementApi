using AutoMapper;
using Core.Services;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models.Request;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Moq;

namespace Testes.Services
{
    public class LoginServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LoginService _loginService;
        private readonly Mock<IOptions<AppSettingsConfig>> _settingsMock;

        public LoginServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _settingsMock = new Mock<IOptions<AppSettingsConfig>>();

            var appSettings = new AppSettingsConfig
            {
                AppSettings = new AppSettings { Security = new Security { Key = "SecretKeyActEmployeeManagementApi" } }
            };

            _settingsMock.Setup(s => s.Value).Returns(appSettings);

            _loginService = new LoginService(_unitOfWorkMock.Object, _mapperMock.Object, _settingsMock.Object);
        }
        [Fact]
        public async Task Login_ShouldReturnUserResponse_WhenCredentialsAreValid()
        {
            // Simulando usuário no banco de dados
            var fakeUser = new User
            {
                CodUsuario = 1,
                Cpf = "12345678900",
                SenhaCripto = BCrypt.Net.BCrypt.HashPassword("password"),
                Roles = "ADM"
            };

            var fakeEmployee = new Employee { CodUsuario = 1, Nome = "Funcionário Teste" };

            var request = new LoginRequest { Cpf = "12345678900", Password = "password" };

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByCpf(request.Cpf)).ReturnsAsync(fakeUser);
            _unitOfWorkMock.Setup(u => u.EmployeeRepository.GetEmployeeByCodUsuarioAsync(fakeUser.CodUsuario)).ReturnsAsync(fakeEmployee);

            // Chama o serviço de login
            var result = await _loginService.Login(request);

            // Validações
            Assert.NotNull(result);
            Assert.Equal(fakeUser.Cpf, result.Cpf);
            Assert.Equal(fakeEmployee.Nome, result.Nome);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenCpfNotFound()
        {
            var request = new LoginRequest { Cpf = "00000000000", Password = "password" };

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByCpf(request.Cpf)).ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<CustomException>(() => _loginService.Login(request));

            Assert.Equal("Não foi encontrado registro com o cpf informado!", exception.Message);
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenPasswordIsInvalid()
        {
            var fakeUser = new User
            {
                CodUsuario = 1,
                Cpf = "12345678900",
                SenhaCripto = BCrypt.Net.BCrypt.HashPassword("password"),
                Roles = "ADM"
            };

            var request = new LoginRequest { Cpf = "12345678900", Password = "wrongpassword" };

            var employeeRepoMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock.Setup(u => u.EmployeeRepository).Returns(employeeRepoMock.Object);

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByCpf(request.Cpf)).ReturnsAsync(fakeUser);

            var exception = await Assert.ThrowsAsync<CustomException>(() => _loginService.Login(request));

            Assert.Equal("Senha inválida!", exception.Message);
        }


    }
}
