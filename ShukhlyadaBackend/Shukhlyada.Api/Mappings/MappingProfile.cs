using AutoMapper;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Api.DTOs.Account;
using Shukhlyada.Api.DTOs.Channel;
using Shukhlyada.Api.DTOs.Comment;
using Shukhlyada.Api.DTOs.Post;
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
            CreateMap<Channel, ReadChannelWithoutPostsDTO>();
            CreateMap<Channel, ReadChannelWithPostsDTO>();

            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, ReadPostDTO>()
                .ForMember(d => d.Likes, 
                o => o.MapFrom(post => post.UsersLiked != null ? post.UsersLiked.Count() : 0));
            CreateMap<Post, ReadPostWithCommentsDTO>();


            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<Comment, ReadCommentDTO>();

            
            
        }
    }
}
