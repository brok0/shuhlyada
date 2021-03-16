using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Models
{
    public class AccessLevel
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        //"delete posts;ban users;rename channel;add moderators;..."
        public string Permissions { get; set; }
    }
}