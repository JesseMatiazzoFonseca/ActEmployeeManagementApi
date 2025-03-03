using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Funcionario")]
    public class Employee
    {
        [Key]
        public int CodFuncionario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CodUsuario { get; set; }
        public string NomeGestor { get; set; }
    }
}
