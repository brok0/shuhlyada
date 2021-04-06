using System;
using System.Collections.Generic;
using System.Text;
using Shukhlyada.Domain.Abstractions;

namespace Shukhlyada.Domain.Models
{
    public class Account : IEntity<Guid>
    {
        public Guid Id { get; set; } // --this is channel name/ channel name is unique so it goes like identifier
        public string Username { get; set; }
        public string Email { get; set; }
        public int ProfilePictureId { get; set; }
        public DateTime RegisterDate { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }

        public AccountType Type { get; set; }
        
        public ICollection<Channel> Subscriptions { get; set; }
        public ICollection<AccessLevel> PermissionsInChannels { get; set; }
        public ICollection<Post> LikedPosts { get; set; }
        public ICollection<Comment> LikedComments { get; set; }
        public ICollection<Post> CreatedPosts { get; set; }
        public ICollection<Comment> CreatedComments { get; set; }


        //probably useless collections
        public ICollection<Channel> ChannelsWithPermissions { get; set; }
        public ICollection<Channel> PostedInChannels { get; set; }
        public ICollection<Post> CommentedOn { get; set; }
    }

    public enum AccountType
    {
        Admin,
        User
    }
}
