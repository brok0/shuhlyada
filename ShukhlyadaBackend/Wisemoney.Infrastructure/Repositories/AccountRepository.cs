using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account,Guid>,IAccountRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public AccountRepository(AppDbContext context) : base(context)
        { }
    }
}
