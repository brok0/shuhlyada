﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs
{
    public class CommentCreateDTO
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }

    }
}
