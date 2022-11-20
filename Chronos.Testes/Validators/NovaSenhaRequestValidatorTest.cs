using Chronos.Services.Validators;
using Chronos.Testes.Fakers;
using FluentValidation.TestHelper;

namespace Chronos.Testes.Validators
{
    [TestClass]
    public class NovaSenhaRequestValidatorTest
    {
        private readonly NovaSenhaRequestValidator _validator = new NovaSenhaRequestValidator();

        [TestMethod]
        public async Task TestValido()
        {
            var request = NovaSenhaRequestFaker.GetRequest();
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public async Task TestCodigoInvalido()
        {
            var request = NovaSenhaRequestFaker.GetRequest();
            request.Codigo = "";
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.Codigo);

            request.Codigo = "123";
            var result2 = await _validator.TestValidateAsync(request);
            result2.ShouldHaveValidationErrorFor(p => p.Codigo);
        }

        [TestMethod]
        public async Task TestSenhaInvalida()
        {
            var request = NovaSenhaRequestFaker.GetRequest();
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
