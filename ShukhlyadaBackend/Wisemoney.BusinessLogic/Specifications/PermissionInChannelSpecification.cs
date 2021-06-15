using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class PermissionInChannelSpecification : Specification<Account>
    {
        public PermissionInChannelSpecification(Guid userId)
        {
            Query.Where(a => a.Id == userId).Include(b=>b.PermissionsInChannels);
        }
    }
}
