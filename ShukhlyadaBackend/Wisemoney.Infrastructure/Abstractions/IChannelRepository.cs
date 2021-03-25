using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Abstractions
{
    public interface IChannelRepository: IRepository<Channel, Guid>
    {
        public Task<Channel> GetByNameAsync(string name);
    }
}
