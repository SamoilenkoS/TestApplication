namespace BussinessLayer.Interfaces
{
    public interface IMailExchangerService
    {
        void SendMessage(string destMail, string messageSubject, string messageBody);
    }
}
