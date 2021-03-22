using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class UserNotFoundException : HttpException
    {
        public UserNotFoundException() : base(HttpStatusCode.NotFound, "User with given id not found")
        {
        }
    }
}
