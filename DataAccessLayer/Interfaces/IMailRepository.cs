using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IMailRepository
    {
        void SaveMail(EmailDTO email);
    }
}
