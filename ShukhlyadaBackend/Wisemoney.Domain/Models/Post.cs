using Shukhlyada.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Models
{
    public class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Account> UsersLiked { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Report> Reports { get; set; }

        //probably useless collections
        public ICollection<Account> UsersCommented { get; set; }
    }
}
