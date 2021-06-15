using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
    public class ReportsNotFoundException : HttpException
    {
        public ReportsNotFoundException() :base(System.Net.HttpStatusCode.NotFound,"No reports for this post")
        {

        }
    }
}
