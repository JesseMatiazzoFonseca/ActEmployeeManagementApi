
using System.Data.SqlClient;

namespace Data.Repository
{
    public class BaseRepository
    {
        public SqlConnection SqlConnection { get; set; }
        public SqlTransaction SqlTransaction { get; set; }

        public BaseRepository(SqlConnection sqlConnection)
        {
            this.SqlConnection = sqlConnection;
        }

        public SqlCommand CreateCommand(string query)
        {
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
            command.Connection = SqlConnection;
            command.CommandText = query;

            if (SqlTransaction != null)
                command.Transaction = SqlTransaction;

            return command;
        }
    }
}
