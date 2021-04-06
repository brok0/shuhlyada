using Microsoft.EntityFrameworkCore;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class ChannelRepository: BaseRepository<Channel, string>,IChannelRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public ChannelRepository(AppDbContext context) : base(context)
        { }


    }
}
