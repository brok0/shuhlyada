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
        }
    }
}
