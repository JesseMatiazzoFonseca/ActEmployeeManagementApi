using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByCpf(string cpf);
        public int PostUser(User request);
        public bool DisableUsuario(int codUsuario);
    }
}
