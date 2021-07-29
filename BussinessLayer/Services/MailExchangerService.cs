using BussinessLayer.Interfaces;
using BussinessLayer.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BussinessLayer.Services
{
    public class MailExchangerService : IMailExchangerService
    {
        private readonly SmtpOptions _smtpOptions;

        public MailExchangerService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public void SendMessage(string destMail, string messageSubject, string messageBody)
        {
            var from = new MailAddress(_smtpOptions.SenderMail, _smtpOptions.SenderName);
            using var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpOptions.SenderMail,
                      _smtpOptions.SenderPassword)
            };
            var to = new MailAddress(destMail);
            var message = new MailMessage(from, to)
            {
                Body = messageBody,
                Subject = messageSubject
            };

            client.Send(message);
        }
    }
}
