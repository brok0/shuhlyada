using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class PostRepository:BaseRepository<Post, Guid>,IPostRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public PostRepository(AppDbContext context) : base(context)
        { }

    }
}
