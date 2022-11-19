namespace Chronos.Testes.Fakers
{
    public static class UsuarioFaker
    {
        private static Faker fake = new Faker();

        public static Usuario GetUsuario()
        {
            return new Usuario()
            {
                Id = fake.UniqueIndex,
                Nome = fake.Person.FullName,
                DataAlteracao = fake.Date.Recent(),
                DataInclusao = fake.Date.Recent(),
                ConfirmacaoToken = fake.Random.String(),
                Confirmado = fake.Random.Bool(),
                Email = fake.Person.Email,
                Permissao = fake.PickRandom<Permissao>(),
                Senha = fake.Internet.Password(8, true, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"),
                ResetSenhaToken = fake.Random.String(),
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
