using Infrastructure.Models.MailModels;

namespace Infrastructure.Services.Interfaces
{
    public interface IMailService
    {
        public Task SendEmailAsync(MailRequest mailRequest);
    }
}
