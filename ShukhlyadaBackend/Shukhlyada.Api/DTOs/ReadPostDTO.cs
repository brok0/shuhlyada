using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs
{
    public class ReadPostDTO
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ChannelId { get; set; }
       
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Account> UsersLiked { get; set; } // to see who liked
        public ICollection<Comment> Comments { get; set; } 
        
        //probably useless collections
        public ICollection<Account> UsersCommented { get; set; } // to see who commented (??)
    }
}
