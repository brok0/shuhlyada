using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Abstractions
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        public Task<Account> GetByEmailAsync(string email);
    }
}
