using System;
using System.Collections.Generic;
using System.Text;
using Shukhlyada.Domain.Abstractions;

namespace Shukhlyada.Domain.Models
{
    public class Channel : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Account> Subscribers { get; set; }
        public ICollection<AccessLevel> UsersPermissions { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Report> Reports { get; set; }
        


        //probably useless collections
        public ICollection<Account> UsersWithPermissions { get; set; }
        public ICollection<Account> UsersThatPosted { get; set; }
    }
}
