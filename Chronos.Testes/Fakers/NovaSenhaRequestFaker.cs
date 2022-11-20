using Chronos.Domain.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Testes.Fakers
{
    public static class NovaSenhaRequestFaker
    {
        private static Faker fake = new Faker();

        public static NovaSenhaRequest GetRequest()
        {
            return new NovaSenhaRequest
            {
                Codigo = fake.Random.String(6),
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
    }
}
