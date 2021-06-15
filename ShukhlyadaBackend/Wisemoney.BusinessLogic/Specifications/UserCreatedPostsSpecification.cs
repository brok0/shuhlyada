using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class UserCreatedPostsSpecification : Specification<Account>
    {
        public UserCreatedPostsSpecification(Guid userId) //int page, int numberOfPages
        {
            Query.Where(u => u.Id == userId).Include(l => l.CreatedPosts);
            //Query.Where(u => u.Id == userId).Include(l => l.CreatedPosts).OrderBy(p => p.PublishedDate).Skip(page * numberOfPages).Take(numberOfPages);
        }
    }
}
