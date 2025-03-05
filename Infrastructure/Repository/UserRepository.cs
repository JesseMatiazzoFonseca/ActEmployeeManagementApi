using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            string query = @"SELECT * FROM USUARIO";
            return await SqlConnection.QueryAsync<User>(query, SqlTransaction);
        }

        public int PostUser(User request)
        {
            return (int)SqlConnection.Insert(request, SqlTransaction);
        }

        public bool DisableUsuario(int codUsuario)
        {
            string query = @"UPDATE USUARIO SET STATUS = 0 WHERE CODUSUARIO = @CODUSUARIO";
            return SqlConnection.Execute(query, new { codUsuario }, SqlTransaction) > 0;
        }

        public async Task<User> GetUserByCpf(string cpf)
        {
            string query = @"SELECT * FROM USUARIO WHERE CPF = @CPF";
            return await SqlConnection.QueryFirstOrDefaultAsync<User>(query, new { cpf }, SqlTransaction);
        }
        public bool TransformManager(int codUsuario)
        {
            string query = @"UPDATE USUARIO SET ROLES = 'GESTOR' WHERE CODUSUARIO=@CODUSUARIO";
            return SqlConnection.Execute(query, new { codUsuario }, SqlTransaction) > 0;
        }
    }
}
