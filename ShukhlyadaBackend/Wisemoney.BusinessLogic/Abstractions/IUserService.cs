using System;
using System.Collections.Generic;
using System.Text;
using Shukhlyada.Domain.Models;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<Account> AuthenticateAsync(string email, string password);
        Task<Account> CreateAccountAsync(Account acc);
        Task<List<Channel>> GetSubscribedChannelsAsync(Guid userId);
    }
}
