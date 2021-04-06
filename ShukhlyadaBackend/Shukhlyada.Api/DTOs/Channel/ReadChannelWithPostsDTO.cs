using Shukhlyada.Api.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Channel
{
    public class ReadChannelWithPostsDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<ReadPostDTO> Posts { get; set; }
    }
}
