using Chronos.Services.Validators;
using Chronos.Testes.Fakers;
using FluentValidation.TestHelper;

namespace Chronos.Testes.Validators
{
    [TestClass]
    public class UsuarioRequestValidatorTest
    {
        private readonly UsuarioRequestValidator _validator = new UsuarioRequestValidator();

        [TestMethod]
        public async Task TestValido()
        {
            var request = UsuarioToContractFaker.GetRequest();
            var result = await _validator.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public async Task TestEmailInvalido()
        {
            var request = UsuarioToContractFaker.GetRequest();
            request.Email = string.Empty;
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.Email);

            request.Email = "email.com";
            var result2 = await _validator.TestValidateAsync(request);
            result2.ShouldHaveValidationErrorFor(p => p.Email);
        }

        [TestMethod]
        public async Task TestSenhaInvalida()
        {
            var request = UsuarioToContractFaker.GetRequest();
            request.Senha = "";
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.Senha);

            request.Senha = "a";
            var result2 = await _validator.TestValidateAsync(request);
            result2.ShouldHaveValidationErrorFor(p => p.Senha);

            request.Senha = "aaaaaaaa";
            var result3 = await _validator.TestValidateAsync(request);
            result3.ShouldHaveValidationErrorFor(p => p.Senha);

            request.Senha = "AAAAAAAA";
            var result4 = await _validator.TestValidateAsync(request);
            result4.ShouldHaveValidationErrorFor(p => p.Senha);

            request.Senha = "11111111";
            var result5 = await _validator.TestValidateAsync(request);
            result5.ShouldHaveValidationErrorFor(p => p.Senha);

            request.Senha = "AAAAaaaa";
            var result6 = await _validator.TestValidateAsync(request);
            result6.ShouldHaveValidationErrorFor(p => p.Senha);
        }
    }
}
