using Microsoft.Extensions.Options;
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
        private readonly IOptions<ElasticEmailCredentials> _elasticEmailCredentials;

        public MailService(IOptions<ElasticEmailCredentials> elasticEmailCredentials)
        {
            _elasticEmailCredentials = elasticEmailCredentials;
        }

        public async Task SendMailAsync(string receiver, string subject, string body, bool isHtml)
        {
            var credentials = _elasticEmailCredentials.Value;

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Shukhlyada", credentials.Username));
            emailMessage.To.Add(new MailboxAddress("", receiver));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(isHtml?MimeKit.Text.TextFormat.Html:MimeKit.Text.TextFormat.Plain)
            {
                Text = body,
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.elasticemail.com", 2525, true);
                await client.AuthenticateAsync(credentials.Username, credentials.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
