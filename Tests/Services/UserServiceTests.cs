using AutoMapper;
using Core.Services;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models.Request;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System.Data;

namespace Testes.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IOptions<AppSettingsConfig>> _settingsMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _settingsMock = new Mock<IOptions<AppSettingsConfig>>();

            var appSettings = new AppSettingsConfig
            {
                AppSettings = new AppSettings { Security = new Security { PasswordAdm = "Act1234@" } }
            };
            _settingsMock.Setup(s => s.Value).Returns(appSettings);

            _userService = new UserService(_unitOfWorkMock.Object, _mapperMock.Object, _settingsMock.Object);
        }
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            var fakeUsers = new List<User>
            {
                new() { CodUsuario = 1, Cpf = "12345678900", Roles = "GESTOR" },
                new() { CodUsuario = 2, Cpf = "98765432100", Roles = "USUARIO" }
            };

            _unitOfWorkMock.Setup(u => u.UserRepository.GetAllUsersAsync()).ReturnsAsync(fakeUsers);
            var result = await _userService.GetAllUsersAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task PostUser_ShouldReturnUserId()
        {
            var request = new UserRequest
            {
                Cpf = "12345678900",
                Password = "password",
                Roles = "USUARIO"
            };

            var fakeUser = new User { CodUsuario = 1, Cpf = request.Cpf, Roles = request.Roles };

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByCpf(request.Cpf)).ReturnsAsync((User)null);
            _unitOfWorkMock.Setup(u => u.RolesRepository.GetAllRolesAsync())
                          .ReturnsAsync(new List<Roles> { new() { Nivel = 3, Descricao = "USUARIO" } });

            _mapperMock.Setup(m => m.Map<User>(It.IsAny<UserRequest>())).Returns(fakeUser);
            _unitOfWorkMock.Setup(u => u.UserRepository.PostUser(It.IsAny<User>())).Returns(1);

            var result = await _userService.PostUser(request);

            Assert.Equal(1, result);
        }
        [Fact]
        public async Task PostUser_ShouldThrowException_WhenCpfAlreadyExists()
        {
            var rolesRepoMock = new Mock<IRolesRepository>();
            _unitOfWorkMock.Setup(u => u.RolesRepository).Returns(rolesRepoMock.Object);

            var request = new UserRequest
            {
                Cpf = "12345678900",
                Password = "password",
                Roles = "USER"
            };

            var existingUser = new User { CodUsuario = 1, Cpf = request.Cpf, Roles = "USER" };

            _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByCpf(request.Cpf)).ReturnsAsync(existingUser);

            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.PostUser(request));

            Assert.Equal("CPF já cadastrado", exception.Message);
        }
        [Fact]
        public void DisableUser_ShouldReturnTrue()
        {
            _unitOfWorkMock.Setup(u => u.UserRepository.DisableUsuario(It.IsAny<int>())).Returns(true);

            var result = _userService.DisableUser(1);

            Assert.True(result);
        }



    }
}
