using Chronos.Domain.Contracts.Request;

namespace Chronos.Testes.Fakers
{
    public static class TarefaToContractFaker
    {
        private static Faker fake = new Faker();

        public static TarefaRequest GetRequest()
        {
            return new TarefaRequest()
            {
                DataFinal = fake.Date.Future(),
                DataInicial = fake.Date.Past(),
                Descricao = fake.Random.String(),
                ProjetoId = fake.Random.Int(),
            };
        }
    }
}
