using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Account
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
