using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Infrastructure.Repositories
{
    class ReportRepository:BaseRepository<Report, Guid>, IReportRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public ReportRepository(AppDbContext context) : base(context)
        { }

    }
}
