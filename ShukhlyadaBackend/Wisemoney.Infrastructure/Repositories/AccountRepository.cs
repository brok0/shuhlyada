using Microsoft.EntityFrameworkCore;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account,Guid>,IAccountRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public AccountRepository(AppDbContext context) : base(context)
        { }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
