using DataAccessLayer.Models;

namespace BussinessLayer.Interfaces
{
    public interface IMailService
    {
        void SaveMailAddress(EmailDTO email);
    }
}
