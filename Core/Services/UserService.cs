using AutoMapper;
using BCrypt.Net;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Core.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly AppSettingsConfig _appSettings;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettingsConfig> settings) : base(unitOfWork, mapper)
        {
            _appSettings = settings.Value;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllUsersAsync();
        }

        public async Task<int> PostUser(UserRequest request)
        {
            request = await ValidateUser(request);
            return _unitOfWork.UserRepository.PostUser(_mapper.Map<User>(request));
        }

        public bool DisableUser(int codUsuario)
        {
            return _unitOfWork.UserRepository.DisableUsuario(codUsuario);
        }

        private async Task<UserRequest> ValidateUser(UserRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByCpf(request.Cpf);
            var roles = await _unitOfWork.RolesRepository.GetAllRolesAsync();
            if (user != null)
                throw new CustomException("CPF já cadastrado");
            if (string.IsNullOrEmpty(request.Roles) || (_roles is null || !_roles.Contains("ADM")))
            {
                if (request.Password == _appSettings.AppSettings.Security.PasswordAdm)
                    request.Roles = roles.OrderBy(x => x.Nivel).FirstOrDefault().Descricao;
                else
                    request.Roles = roles.OrderBy(x => x.Nivel).LastOrDefault().Descricao;
            }
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            return request;
        }
    }
}
