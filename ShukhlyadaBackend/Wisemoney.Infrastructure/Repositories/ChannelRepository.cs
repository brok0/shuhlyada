using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class ChannelRepository: BaseRepository<Channel, Guid>,IChannelRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public ChannelRepository(AppDbContext context) : base(context)
        { }

    }
}
