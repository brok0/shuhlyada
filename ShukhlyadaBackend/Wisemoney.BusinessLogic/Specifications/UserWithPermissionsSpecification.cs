using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class UserWithPermissionsSpecification : Specification<Account>
    {
        public UserWithPermissionsSpecification(Guid userId)
        {
            Query.Where(a => a.Id.Equals(userId)).Include(a => a.PermissionsInChannels);
        }
    }
}
