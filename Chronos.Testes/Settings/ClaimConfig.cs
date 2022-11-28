using System.Security.Claims;

namespace Chronos.Testes.Settings
{
    public static class ClaimConfig
    {
        public static IEnumerable<Claim> Get(int id, string email, Permissao permissao)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, permissao.ToString())
            };
        }
    }
}
