using Dapper.Contrib.Extensions;

namespace Domain.Entities
{

    [Table("USUARIO")]
    public class User
    {
        [Key]
        public int CodUsuario { get; set; }
        public string Cpf { get; set; }
        public string SenhaCripto { get; set; }
        public string Roles { get; set; }
        public bool Status { get; set; } = true;
    }
}
