using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Exceptions
{
   public class UserDontHavePermissionException : HttpException
    {
        public UserDontHavePermissionException() :base(System.Net.HttpStatusCode.Forbidden,"User not allowed to do this.")
        {

        }
    }
}
