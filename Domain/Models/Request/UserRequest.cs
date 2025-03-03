using Domain.Entities;
using Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Request
{
    public class UserRequest : IValidatableObject
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }
        public string Roles { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!RequestValidator.CpfValidator(Cpf))
                yield return new ValidationResult("CPF inválido", [nameof(Cpf)]);
        }
    }
}
