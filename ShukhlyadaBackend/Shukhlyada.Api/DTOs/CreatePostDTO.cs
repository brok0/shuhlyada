using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs
{
    public class CreatePostDTO
    {        
        public Guid ChannelId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
