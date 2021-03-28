using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class PostsInChannelSpecification : Specification<Post>
    {
        public PostsInChannelSpecification(Guid channelId)
        {
            Query.Where(p => p.ChannelId == channelId);
        }
    }
}
