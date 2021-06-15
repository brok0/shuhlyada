using AutoMapper;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Api.DTOs.Account;
using Shukhlyada.Api.DTOs.Channel;
using Shukhlyada.Api.DTOs.Comment;
using Shukhlyada.Api.DTOs.Post;
using Shukhlyada.Api.DTOs.Reports;
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
            CreateMap<Channel, ReadChannelWithPostsDTO>().ForMember(s=>s.Subscribers,o => o.MapFrom(c=>c.Subscribers.Count() ));

            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, ReadPostDTO>()
                .ForMember(d => d.Likes, 
                o => o.MapFrom(post => post.UsersLiked != null ? post.UsersLiked.Count() : 0))
                .ForMember(u => u.AccountName, p=> p.MapFrom(a => a.Account.Username));
            CreateMap<Post, ReadPostWithCommentsDTO>();


            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<Comment, ReadCommentDTO>();

            CreateMap<CreateReportDTO, Report>();
            CreateMap<Report,ReadReportDTO>();

        }
    }
}
