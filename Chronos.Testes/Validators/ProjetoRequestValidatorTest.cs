using Chronos.Domain.Contracts.Request;
using Chronos.Services.Validators;
using Chronos.Testes.Fakers;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Testes.Validators
{
    [TestClass]
    public class ProjetoRequestValidatorTest
    {
        private readonly ProjetoRequestValidator _validator = new ProjetoRequestValidator();

        [TestMethod]
        public async Task TestValido()
        {
            var request = ProjetoToContractFaker.GetRequest();

            var result = await _validator.TestValidateAsync(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public async Task TestNomeInvalido()
        {
            var request = ProjetoToContractFaker.GetRequest();
            request.Nome = "";
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.Nome);

            request.Nome = "aa";
            var result2 = await _validator.TestValidateAsync(request);
            result2.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        [TestMethod]
        public async Task TestDataFimMenorQueDataInicio()
        {
            var request = ProjetoToContractFaker.GetRequest();
            request.DataInicio = request.DataFim.AddDays(2);
            var result = await _validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(p => p.DataFim);
        }
    }
}
