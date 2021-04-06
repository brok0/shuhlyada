using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class ChannelAlreadyExistException : HttpException
    {
        public ChannelAlreadyExistException() : base(HttpStatusCode.BadRequest, "Channel with this name already exists")
        {

        }
    }
}
