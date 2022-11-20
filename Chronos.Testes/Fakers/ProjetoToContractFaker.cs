using Chronos.Domain.Contracts.Request;

namespace Chronos.Testes.Fakers
{
    public static class ProjetoToContractFaker
    {
        private static Faker _fake = new Faker();

        public static ProjetoRequest GetRequest()
        {
            return new ProjetoRequest()
            {
                DataFim = _fake.Date.Future(),
                DataInicio = _fake.Date.Past(),
                Nome = _fake.Commerce.Product()
            };
        }
    }
}
