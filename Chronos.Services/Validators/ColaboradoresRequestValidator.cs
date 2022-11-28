using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class ColaboradoresRequestValidator : AbstractValidator<ColaboradoresRequest>
    {
        public ColaboradoresRequestValidator()
        {
            RuleFor(p => p.Usuarios)
                .NotEmpty()
                .WithMessage("Lista de colaboradores não pode ser nula.")
                .Must(p => !p.Contains(0))
                .WithMessage("Id '0' não é valido.");
        }
    }
}
