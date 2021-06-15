using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class ChannelWithPostsSpecification : Specification<Channel>
    {
        public ChannelWithPostsSpecification(string channelName)
        {
            Query.Where(c => c.Id == channelName).Include(s=>s.Subscribers).Include(c => c.Posts).ThenInclude(p => p.UsersLiked);
        }
    }
}
