using BusinessLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLayer.Helpers.Interfaces
{
    public interface IMailService
    {
        void SaveMailAddress(EmailDTO email);
        bool ConfirmMail(ConfirmationMessageModel model);
    }
}
