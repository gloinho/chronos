using Chronos.Domain.Contracts.Request;

namespace Chronos.Testes.Fakers
{
    public static class NovaSenhaRequestFaker
    {
        private static Faker fake = new Faker();

        public static NovaSenhaRequest GetRequest()
        {
            return new NovaSenhaRequest
            {
                Codigo = fake.Random.Int(100000, 999999).ToString(),
                Senha = fake.Internet.Password(
                    8,
                    true,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                ),
                ConfirmacaoSenha = fake.Internet.Password(
                    8,
                    true,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                ),
            };
        }

        public static NovaSenhaRequest GetRequest(string senha)
        {
            return new NovaSenhaRequest
            {
                Codigo = fake.Random.Int(100000, 999999).ToString(),
                Senha = senha,
                ConfirmacaoSenha = senha
            };
        }
    }
}
