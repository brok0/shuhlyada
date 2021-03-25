using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Abstractions
{
    public interface IChannelService
    {
        public Task<Channel> CreateChannelAsync(Channel channel, Guid creatorId);
        public Task<Channel> GetChannelByNameAsync(string name);
    }
}
