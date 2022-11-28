using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;

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

        public static UsuarioResponse GetResponse(Usuario usuario)
        {
            return new UsuarioResponse()
            {
                Email = usuario.Email,
                Id = usuario.Id,
                Nome = usuario.Nome,
            };
        }

        public static List<UsuarioResponse> GetResponses(List<Usuario> usuarios)
        {
            var lista = new List<UsuarioResponse>();
            foreach (var usuario in usuarios)
            {
                lista.Add(GetResponse(usuario));
            }
            return lista;
        }
    }
}
