using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly EFCoreContext _dbContext;
        public MailRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ConfirmMail(EmailDTO email)
        {
            var entity = _dbContext.Emails.Where(x =>
                x.UserId == email.UserId && 
                x.ConfirmationMessage == email.ConfirmationMessage).FirstOrDefault();

            if (entity != null)
            {
                entity.IsConfirmed = true;
                _dbContext.SaveChanges();
            }

            return entity != null;
        }

        public void SaveMail(EmailDTO email)
        {
            _dbContext.Emails.Add(email);
            _dbContext.SaveChanges();
        }
    }
}
