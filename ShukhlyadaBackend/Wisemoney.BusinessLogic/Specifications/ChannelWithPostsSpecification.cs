using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class ChannelWithPostsSpecification : Specification<Post>
    {
        public ChannelWithPostsSpecification(string channelName)
        {
            Query.Where(p => p.ChannelId == channelName);
        }
    }
}
