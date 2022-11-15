using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class TarefaRequestValidator : AbstractValidator<TarefaRequest>
    {
        public TarefaRequestValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty()
                .WithMessage("O campo 'Descricao' não pode estar vazio.");
            RuleFor(p => p.ProjetoId)
                .NotEmpty()
                .WithMessage("O campo 'ProjetoId' não pode estar vazio.");
            RuleFor(p => p.DataFinal)
                .GreaterThan(p => p.DataInicial)
                .WithMessage(
                    "O campo 'DataFim' precisa de uma data posterior ao campo 'DataInicio'."
                );
        }
    }
}
