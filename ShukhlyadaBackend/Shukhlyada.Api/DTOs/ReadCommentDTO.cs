using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs
{
    public class ReadCommentDTO
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        
        public Guid AccountId { get; set; }
       
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

      
    }
}
