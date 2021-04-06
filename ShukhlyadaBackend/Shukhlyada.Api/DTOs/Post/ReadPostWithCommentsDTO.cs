using Shukhlyada.Api.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Post
{
    public class ReadPostWithCommentsDTO
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string ChannelId { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }

        public ICollection<ReadCommentDTO> Comments { get; set; }

    }
}
