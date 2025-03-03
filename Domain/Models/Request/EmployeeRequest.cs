using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Request
{
    public class EmployeeRequest : IValidatableObject
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("^\\d{2}9\\d{7}$", ErrorMessage = "Insira um número de telefone válido exemplo: 11999999999")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("^\\d{2}9\\d{8}$", ErrorMessage = "Insira um número de celular válido exemplo: 11999999999")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string NomeGestor { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório.")]
        public UserRequest User { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ValidateIFunderage(DataNascimento))
                yield return new ValidationResult("Funcionário menor de idade não pode ser cadastrado", [nameof(DataNascimento)]);
        }
        private bool ValidateIFunderage(DateTime dataNacimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNacimento.Year;
            if (dataNacimento.Date > hoje.AddYears(-idade))
                idade--;
            return idade < 18;
        }
    }
}
