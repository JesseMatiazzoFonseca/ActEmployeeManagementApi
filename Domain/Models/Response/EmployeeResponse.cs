using System.Text.RegularExpressions;

namespace Domain.Models.Response
{
    public class EmployeeResponse
    {
        private string _celular;
        private string _telefone;
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone
        {
            get => Regex.Replace(_telefone ?? "", @"\D", "");
            set => _telefone = value;
        }
        public string Celular
        {
            get => Regex.Replace(_celular ?? "", @"\D", "");
            set => _celular = value;
        }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string DtNascimentoAux { get { return DataNascimento.ToString("yyyy/MM/dd"); } }
        public DateTime DataNascimento { get; set; }
        public string NomeGestor { get; set; }
        public string Cpf { get; set; }
        public string Roles { get; set; }
    }
}
