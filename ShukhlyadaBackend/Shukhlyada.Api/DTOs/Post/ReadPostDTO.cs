using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Post
{
    public class ReadPostDTO
    {
        public Guid Id { get; set; }

        public string AccountName { get; set; }

        public string ChannelId { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }

        //isliked prop to restrict user from spamming likes
    }
}
