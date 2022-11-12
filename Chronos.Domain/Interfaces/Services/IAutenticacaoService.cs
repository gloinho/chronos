namespace Chronos.Domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<string> Login(string email, string senha);
        Task Confirmar(string token);
    }
}
