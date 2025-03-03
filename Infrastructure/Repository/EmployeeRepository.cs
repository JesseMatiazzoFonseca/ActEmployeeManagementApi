using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            string query = "SELECT * FROM FUNCIONARIO";
            return await SqlConnection.QueryAsync<Employee>(query, SqlTransaction);
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
