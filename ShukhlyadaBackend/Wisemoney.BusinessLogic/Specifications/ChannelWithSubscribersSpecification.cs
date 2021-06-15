using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class ChannelWithSubscribersSpecification : Specification<Channel>
    {
        public ChannelWithSubscribersSpecification(string channelName) 
        {
            Query.Where(x => x.Id == channelName).Include(c => c.Subscribers);
        }
    }
}
