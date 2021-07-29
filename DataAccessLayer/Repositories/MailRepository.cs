using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly EFCoreContext _dbContext;
        public MailRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveMail(EmailDTO email)
        {
            _dbContext.Emails.Add(email);
            _dbContext.SaveChanges();
        }
    }
}
