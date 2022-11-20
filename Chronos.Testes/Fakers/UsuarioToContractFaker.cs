using Chronos.Domain.Contracts.Request;

namespace Chronos.Testes.Fakers
{
    public static class UsuarioToContractFaker
    {
        private static Faker fake = new Faker();

        public static UsuarioRequest GetRequest()
        {
            return new UsuarioRequest
            {
                Email = fake.Person.Email,
                Nome = fake.Person.FirstName,
                Senha = "SenhaValida123",
            };
        }
    }
}
