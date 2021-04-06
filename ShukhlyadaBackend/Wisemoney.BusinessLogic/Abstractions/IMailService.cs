using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Abstractions
{
    public interface IMailService
    {
        public Task SendMailAsync(string receiver,string subject,string body,bool isHtml);
    }
}
