using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class CreatorCantLeaveFromChannelException : HttpException
    {
        public CreatorCantLeaveFromChannelException() : base(HttpStatusCode.BadRequest, "Creator can't leave his channel")
        {

        }
    }
}
