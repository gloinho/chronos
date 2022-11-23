using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class ProjetoRequestValidator : AbstractValidator<ProjetoRequest>
    {
        public ProjetoRequestValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O campo 'Nome' não pode estar vazio.")
                .MinimumLength(3)
                .WithMessage("O campo 'Nome' precisa de no minimo 3 caracteres");
            RuleFor(p => p.DataFim)
                .NotEmpty()
                .WithMessage("O campo 'DataFim' não pode estar vazio.'")
                .GreaterThan(p => p.DataInicio)
                .WithMessage(
                    "O campo 'DataFim' precisa de uma data posterior ao campo 'DataInicio'."
                );
        }
    }
}
