using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class UserAlreadyExistException : HttpException
    {
        public UserAlreadyExistException() : base(HttpStatusCode.BadRequest, "User with this login already exists")
        {
        }
    }
}
