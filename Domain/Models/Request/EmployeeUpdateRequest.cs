using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Request
{
    public class EmployeeUpdateRequest : IValidatableObject
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
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
        public UserEmployeeResponse User { get; set; }
        public class UserEmployeeResponse()
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
            public string Cpf { get; set; }
            public string Roles { get; set; }
        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ValidateIFunderage(DataNascimento))
                yield return new ValidationResult("Funcionário menor de idade não pode ser cadastrado", [nameof(DataNascimento)]);
            if (!RequestValidator.CpfValidator(User.Cpf))
                yield return new ValidationResult("CPF inválido", [nameof(User.Cpf)]);
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
