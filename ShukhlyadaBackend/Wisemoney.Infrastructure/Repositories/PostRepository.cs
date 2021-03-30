using Microsoft.EntityFrameworkCore;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Repositories
{
    public class PostRepository:BaseRepository<Post, Guid>,IPostRepository
    {
        public override IUnitOfWork UnitOfWork => (AppDbContext)_context;

        public PostRepository(AppDbContext context) : base(context)
        { }

        public async Task<Post> GetByIdAsync(Guid Id)
        {
            return await _dbSet.Where(x => x.Id.Equals(Id))
                               .Include(x =>  x.UsersLiked)
                               .Include(x => x.Comments)
                               .FirstOrDefaultAsync();
        }
    }
}
