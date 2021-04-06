using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class UserSubscribedChannelsSpecification: Specification<Account>
    {
        public UserSubscribedChannelsSpecification(Guid userId)
        {
            Query.Where(x => x.Id == userId).Include(s => s.Subscriptions);
        } 
    }
}
