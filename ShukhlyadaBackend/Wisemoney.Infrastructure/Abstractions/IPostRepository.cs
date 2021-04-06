﻿using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.Infrastructure.Abstractions
{
    public interface IPostRepository:IRepository<Post, Guid>
    {
        public Task<Post> GetByIdAsync(Guid Id);
    }
}
