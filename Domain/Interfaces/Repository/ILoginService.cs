using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Interfaces.Repository
{
    public interface ILoginService 
    {
        public Task<UserResponse> Login(LoginRequest request);
    }
}
