using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IRolesRepository : IBaseRepository
    {
        public Task<IEnumerable<Roles>> GetAllRolesAsync();
    }
}
