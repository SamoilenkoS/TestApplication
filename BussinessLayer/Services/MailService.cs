﻿using AutoMapper;
using BusinessLayer.Helpers.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.Services
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
