using AutoMapper;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, Account>();
            CreateMap<Account, UserReadDTO>();

            CreateMap<CreateChannelDTO, Channel>();
            CreateMap<Channel, ReadChannelDTO>();

            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, ReadPostDTO>()
                .ForMember(d => d.Likes, 
                o => o.MapFrom(post => post.UsersLiked != null ? post.UsersLiked.Count() : 0));

            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<Comment, ReadCommentDTO>();
        }
    }
}
