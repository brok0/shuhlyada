using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    public class ReportsForOnePostSpecification : Specification<Report>
    {
        public ReportsForOnePostSpecification(Guid PostId)
        {
            Query.Where(p => p.PostId == PostId);
        }
    }
}
