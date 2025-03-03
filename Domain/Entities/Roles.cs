using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("ROLES")]
    public class Roles
    {
        [Key]
        public int CodRoles { get; set; }
        public string Descricao { get; set; }
        public int Nivel { get; set; }
    }
}
