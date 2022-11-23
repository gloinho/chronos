using Chronos.Domain.Contracts.Request;
using Chronos.Services.Validators;
using FluentValidation.TestHelper;

namespace Chronos.Testes.Validators
{
    [TestClass]
    public class AdicionarColaboradoresRequestValidatorTest
    {
        private readonly ColaboradoresRequestValidator _validator =
            new ColaboradoresRequestValidator();

        [TestMethod]
        public async Task TestAdicionarColaboradores()
        {
            var colaboradores = new ColaboradoresRequest()
            {
                Usuarios = new List<int> { 1, 2 },
            };

            var result = _validator.TestValidate(colaboradores);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public async Task TestAdicionarNenhumColaborador()
        {
            var colaboradores = new ColaboradoresRequest() { Usuarios = new List<int>() };
            var result = _validator.TestValidate(colaboradores);
            result.ShouldHaveValidationErrorFor(p => p.Usuarios);
        }

        [TestMethod]
        public async Task TestAdicionarColaboradorComIdZero()
        {
            var colaboradores = new ColaboradoresRequest() { Usuarios = new List<int>() { 0 } };
            var result = _validator.TestValidate(colaboradores);
            result.ShouldHaveValidationErrorFor(p => p.Usuarios);
        }
    }
}
