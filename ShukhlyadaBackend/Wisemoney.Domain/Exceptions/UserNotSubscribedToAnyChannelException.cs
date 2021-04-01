using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace Shukhlyada.Domain.Exceptions
{
    public class UserNotSubscribedToAnyChannelException : HttpException
    {
        public UserNotSubscribedToAnyChannelException() : base(HttpStatusCode.NotFound,"User is not subscribed to any channel.")
        {

        }
    }
}
