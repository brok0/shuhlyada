using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Post
{
    public class CreatePostDTO
    {        
        public string ChannelId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
