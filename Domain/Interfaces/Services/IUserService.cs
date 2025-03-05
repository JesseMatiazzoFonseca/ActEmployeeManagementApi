using Domain.Entities;
using Domain.Models.Request;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<int> PostUser(UserRequest request);
        public bool DisableUser(int codUsuario);
        public bool TransformManager(int codUsuario);
    }
}
