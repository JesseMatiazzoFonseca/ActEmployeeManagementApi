

using System.Data.SqlClient;

namespace Domain.Interfaces.Repository
{
    public interface IBaseRepository
    {
        public SqlTransaction SqlTransaction { get; set; }
    }
}
