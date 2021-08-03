using AutoMapper;
using BussinessLayer.Interfaces;
using BussinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BussinessLayer.Services
{
    public class MailService : IMailService
    {
        private readonly IMailRepository _mailRepository;
        private readonly IMapper _mapper;

        public MailService(IMailRepository mailRepository,
            IMapper mapper)
        {
            _mailRepository = mailRepository;
            _mapper = mapper;
        }

        public bool ConfirmMail(ConfirmationMessageModel model)
        {
            return _mailRepository.ConfirmMail(_mapper.Map<EmailDTO>(model));
        }

        public void SaveMailAddress(EmailDTO email)
        {
            _mailRepository.SaveMail(email);
        }
    }
}
