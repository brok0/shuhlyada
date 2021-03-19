using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Infrastructure.Abstractions
{
    public interface IReportRepository : IRepository<Report,Guid>
    {
    }
}
