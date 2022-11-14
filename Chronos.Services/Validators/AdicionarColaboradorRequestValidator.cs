using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class AdicionarColaboradorRequestValidator
        : AbstractValidator<AdicionarColaboradoresRequest>
    {
        public AdicionarColaboradorRequestValidator()
        {
            RuleFor(p => p.Usuarios).Must(p => !p.Contains(0)).WithMessage("Id '0' não é valido.");
        }
    }
}
