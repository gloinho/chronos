using Chronos.Domain.Entities.Enums;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Chronos.Testes.Fakers
{
    public static class UsuarioFaker
    {
        private static Faker fake = new Faker("pt_BR");

        public static Usuario GetUsuario()
        {
            return new Usuario()
            {
                Id = fake.Random.Int(),
                Nome = fake.Person.FullName,
                DataInclusao = fake.Date.Recent(),
                ConfirmacaoToken = fake.Random.String(),
                Confirmado = fake.Random.Bool(),
                Email = fake.Person.Email,
                Permissao = fake.PickRandom<Permissao>(),
                Senha = fake.Internet.Password(
                    8,
                    true,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                ),
                ResetSenhaToken = fake.Random.Int(100000, 999999).ToString(),
            };
        }

        public static Usuario GetUsuarioSenhaEncriptada(string senha)
        {
            return new Usuario()
            {
                Id = fake.Random.Int(),
                Nome = fake.Person.FullName,
                DataAlteracao = fake.Date.Recent(),
                DataInclusao = fake.Date.Recent(),
                ConfirmacaoToken = fake.Random.String(),
                Confirmado = fake.Random.Bool(),
                Email = fake.Person.Email,
                Permissao = fake.PickRandom<Permissao>(),
                Senha = BCrypt.Net.BCrypt.HashPassword(senha, BCrypt.Net.BCrypt.GenerateSalt()),
                ResetSenhaToken = fake.Random.Int(100000, 999999).ToString(),
            };
        }

        public static Usuario GetUsuario(Permissao permissao)
        {
            return new Usuario()
            {
                Id = fake.Random.Int(),
                Nome = fake.Person.FullName,
                DataAlteracao = fake.Date.Recent(),
                DataInclusao = fake.Date.Recent(),
                ConfirmacaoToken = fake.Random.String(),
                Confirmado = fake.Random.Bool(),
                Email = fake.Person.Email,
                Permissao = permissao,
                Senha = fake.Internet.Password(
                    8,
                    true,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                ),
                ResetSenhaToken = fake.Random.Int(100000, 999999).ToString(),
            };
        }

        public static Usuario GetUsuarioCodigoEncriptado(string codigo)
        {
            return new Usuario()
            {
                Id = fake.Random.Int(),
                Nome = fake.Person.FullName,
                DataAlteracao = fake.Date.Recent(),
                DataInclusao = fake.Date.Recent(),
                ConfirmacaoToken = fake.Random.String(),
                Confirmado = fake.Random.Bool(),
                Email = fake.Person.Email,
                Permissao = fake.PickRandom<Permissao>(),
                Senha = fake.Internet.Password(
                    8,
                    true,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                ),
                ResetSenhaToken = BCrypt.Net.BCrypt.HashPassword(
                    codigo,
                    BCrypt.Net.BCrypt.GenerateSalt()
                ),
            };
        }

        public static List<Usuario> GetUsuarios()
        {
            var lista = new List<Usuario>();
            for (int i = 0; i < 10; i++)
            {
                lista.Add(GetUsuario());
            }
            return lista;
        }
    }
}
