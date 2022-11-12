using Chronos.Domain.Contracts.Request;
using FluentValidation;

namespace Chronos.Services.Validators
{
    public class UsuarioRequestValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioRequestValidator()
        {
            RuleFor(p => p.Email).EmailAddress().WithMessage("Campo 'Email' inválido.");
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("Campo 'Nome' não pode ser vazio.")
                .MinimumLength(3)
                .WithMessage("Campo 'Nome' precisa ter no minimo 3 caracteres.");
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
