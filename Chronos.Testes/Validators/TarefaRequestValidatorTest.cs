using Chronos.Services.Validators;
using Chronos.Testes.Fakers;
using FluentValidation.TestHelper;


namespace Chronos.Testes.Validators
{
    [TestClass]
    public class TarefaRequestValidatorTest
    {
        private readonly TarefaRequestValidator _validator = new TarefaRequestValidator();

        [TestMethod]
        public async Task TestValido()
        {
            var request = TarefaToContractFaker.GetRequest();
            var result = await _validator.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public async Task TestDescricaoVazia()
        {
            var request = TarefaToContractFaker.GetRequest();
            request.Descricao = "";
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.Descricao);
        }

        [TestMethod]
        public async Task TestProjetoIdVazio()
        {
            var request = TarefaToContractFaker.GetRequest();
            request.ProjetoId = 0;
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.ProjetoId);
        }

        [TestMethod]
        public async Task TestDataFinalMenorQueDataInicial()
        {
            var request = TarefaToContractFaker.GetRequest();
            request.DataInicial = request.DataFinal.Value.AddDays(2);
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.DataFinal);
        }
    }
}
