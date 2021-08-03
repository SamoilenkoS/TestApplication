using BussinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class MailExchangerServiceMoq : IMailExchangerService
    {
        public void SendMessage(string destMail, string messageSubject, string messageBody)
        {
            return;
        }
    }
}
