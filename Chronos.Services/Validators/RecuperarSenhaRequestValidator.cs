
using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class RecuperarSenhaRequestValidator : AbstractValidator<NovaSenhaRequest>
    {
        public RecuperarSenhaRequestValidator()
        {
            RuleFor(p => p.Codigo)
                .NotEmpty()
                .WithMessage("O campo 'Codigo' não pode ser vazio.")
                .Length(6)
                .WithMessage("O campo 'Codigo' precisa ter 6 números.");

            RuleFor(p => p.Senha)
                .NotEmpty()
                .WithMessage("O campo 'Senha' não pode ser vazio.")
                .MinimumLength(8)
                .WithMessage("O campo 'Senha' precisa ter no mínimo 8 caracteres.")
                .Matches(@"[A-Z]+")
                .WithMessage("O campo 'Senha' precisa de no minimo uma letra maiuscula.")
                .Matches(@"[a-z]+")
                .WithMessage("O campo 'Senha' precisa de no minimo uma letra minuscula.")
                .Matches(@"[0-9]+")
                .WithMessage("O campo 'Senha' precisa de no minimo um número.");
        }
    }
}
