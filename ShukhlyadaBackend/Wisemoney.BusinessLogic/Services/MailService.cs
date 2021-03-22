using MimeKit;
using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Services
{
    public class MailService:IMailService
    {
        private readonly ElasticEmailCredentials _elasticEmailCredentials;

        public MailService(ElasticEmailCredentials elasticEmailCredentials)
        {
            _elasticEmailCredentials = elasticEmailCredentials;
        }

        public async Task SendMailAsync(string receiver, string subject, string body, bool isHtml)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Shukhlyada", _elasticEmailCredentials.Username));
            emailMessage.To.Add(new MailboxAddress("", receiver));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(isHtml?MimeKit.Text.TextFormat.Html:MimeKit.Text.TextFormat.Plain)
            {
                Text = body,
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.elasticemail.com", 2525, true);
                await client.AuthenticateAsync(_elasticEmailCredentials.Username, _elasticEmailCredentials.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
