using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class PostDeletionException : HttpException
    {
        public PostDeletionException() : base(HttpStatusCode.BadRequest, "Can't delete this post. Its probably already deleted or nonexistent.")
        {

        }
    }
}
