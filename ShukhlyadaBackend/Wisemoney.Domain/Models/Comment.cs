using Shukhlyada.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Models
{
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Account> UsersLiked { get; set; }
    }
}
