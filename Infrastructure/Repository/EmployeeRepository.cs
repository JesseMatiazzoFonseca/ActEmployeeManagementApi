using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Models.Response;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
        {
            string query = @"SELECT * FROM FUNCIONARIO 
                    INNER JOIN USUARIO ON USUARIO.CODUSUARIO = FUNCIONARIO.CODUSUARIO";
            return await SqlConnection.QueryAsync<EmployeeResponse>(query, SqlTransaction);
        }
        public async Task<EmployeeResponse> GetEmployeeAndUserByCodUsuarioAsync(int codUsuario)
        {
            string query = @"SELECT * FROM FUNCIONARIO 
                    INNER JOIN USUARIO ON USUARIO.CODUSUARIO = FUNCIONARIO.CODUSUARIO
                    WHERE FUNCIONARIO.CODUSUARIO = @CODUSUARIO";
            return await SqlConnection.QueryFirstOrDefaultAsync<EmployeeResponse>(query, new { codUsuario }, SqlTransaction);
        }

        public async Task<Employee> GetEmployeeByCodUsuarioAsync(int codUsuario)
        {
            string query = "SELECT * FROM FUNCIONARIO WHERE CODUSUARIO = @CODUSUARIO";
            return await SqlConnection.QueryFirstOrDefaultAsync<Employee>(query, new { codUsuario }, SqlTransaction);
        }

        public int PostEmployee(Employee request)
        {
            return (int)SqlConnection.Insert(request, SqlTransaction);
        }

        public bool PutEmployee(Employee request)
        {
            return SqlConnection.Update(request, SqlTransaction);
        }
    }
}
