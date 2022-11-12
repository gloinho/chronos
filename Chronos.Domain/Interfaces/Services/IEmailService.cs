namespace Chronos.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string html);
    }
}
