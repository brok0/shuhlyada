using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
   public class PostNotFoundException : HttpException
    {
        public PostNotFoundException() : base(System.Net.HttpStatusCode.NotFound,"Post not found")
        {
                
        }
    }
}
