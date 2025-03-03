using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly AppSettingsConfig _appSettings;
        public LoginService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettingsConfig> settings) : base(unitOfWork, mapper)
        {
            _appSettings = settings.Value;
        }

        public async Task<UserResponse> Login(LoginRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByCpf(request.Cpf) ?? throw new CustomException("Não foi encontrado registro com o cpf informado!");
            var funcionario = await _unitOfWork.EmployeeRepository.GetEmployeeByCodUsuarioAsync(user.CodUsuario);
            if (!ValidatePassword(request.Password, user))
                throw new CustomException("Senha inválida!");
            return new UserResponse { Cpf = user.Cpf, Token = GenerateToken(user), Nome = funcionario.Nome };
        }
        private string GenerateToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.AppSettings.Security.Key));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("Id", user.CodUsuario.ToString())]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
            };
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, user.Roles.ToString()));
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool ValidatePassword(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.SenhaCripto);
        }
    }
}
