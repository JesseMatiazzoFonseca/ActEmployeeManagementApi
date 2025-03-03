using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class RolesRepository : BaseRepository, IRolesRepository
    {
        public RolesRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<IEnumerable<Roles>> GetAllRolesAsync()
        {
            string query = "SELECT * FROM ROLES";
            return await SqlConnection.QueryAsync<Roles>(query, SqlTransaction);
        }
    }
}
