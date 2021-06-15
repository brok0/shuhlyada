using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    public class ReportsForPostsInChannelSpecification : Specification<Post>
    {
        public ReportsForPostsInChannelSpecification(string channelId)
        {
            Query.Where(p => p.ChannelId == channelId).Include(r => r.Reports);
        }
    }
}
