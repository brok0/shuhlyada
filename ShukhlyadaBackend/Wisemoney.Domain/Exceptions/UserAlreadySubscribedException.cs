using Shukhlyada.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class UserAlreadySubscribedException : HttpException
    {
        public UserAlreadySubscribedException() : base(HttpStatusCode.NotFound, "User already subscribed.")
        {
        }
    }
}
