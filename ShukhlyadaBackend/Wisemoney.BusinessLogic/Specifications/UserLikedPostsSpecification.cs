using Ardalis.Specification;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Specifications
{
    class UserLikedPostsSpecification : Specification<Post>
    {
        public UserLikedPostsSpecification(Account userId, int page, int numberOfPages)
        {
            //Query.Include(p => p.UsersLiked).Where(p => p.UsersLiked.Contains()).OrderBy(p => p.PublishedDate).Skip(page* numberOfPages).Take(numberOfPages);
        }
    }
}
