using Microsoft.EntityFrameworkCore;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class ChannelRepository: BaseRepository<Channel, Guid>,IChannelRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public ChannelRepository(AppDbContext context) : base(context)
        { }

        public async Task<Channel> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}
