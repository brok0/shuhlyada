using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int ProfilePictureId { get; set; }
        public DateTime RegisterDate { get; set; }
        public AccountType Type { get; set; }
    }
}
