using BusinessLayer.Helpers.Interfaces;

namespace BusinessLayer.Services
{
    public class MailExchangerServiceMoq : IMailExchangerService
    {
        public void SendMessage(string destMail, string messageSubject, string messageBody)
        {
            return;
        }
    }
}
