using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shukhlyada.Api.DTOs;
using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shukhlyada.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;
        private readonly IMapper _mapper;

        public ChannelController(IChannelService channelService, IMapper mapper)
        {
            _channelService = channelService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateChannelAsync(CreateChannelDTO createChannelDTO)
        {
            var channel = _mapper.Map<Channel>(createChannelDTO);
            var createdChannel = await _channelService.CreateChannelAsync(channel, UserId);
            var readChannelDTO = _mapper.Map<ReadChannelDTO>(createdChannel);
            return Ok(readChannelDTO);
        }

        [AllowAnonymous]
        [HttpGet("{channelName}")]
        public async Task<ActionResult> GetChannelByNameAsync(string channelName)
        {
            var channel = await _channelService.GetChannelByNameAsync(channelName);
            if(channel == null)
            {
                return NotFound();
            }
            var readChannelDTO = _mapper.Map<ReadChannelDTO>(channel);
            return Ok(readChannelDTO);
        }
        [Authorize]
        [HttpPost]
        [Route("/post/new")]
        public async Task<IActionResult> CreatePost(CreatePostDTO PostDTO)
        {
            var post = _mapper.Map<Post>(PostDTO);
            var createdPost = await _channelService.CreatePostAsync(post);
            return Ok(createdPost); // незнаю чи варто шось повертити крім екшнрезалту, але хай буде для тесту

        }
        [Authorize]
        [HttpGet]
        [Route("/post/get")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _channelService.GetPostByIdAsync(id);
            
            if(post == null)
            { return NotFound(); }

            var ReadPostDto = _mapper.Map<ReadChannelDTO>(post);
            return Ok(ReadPostDto);
        }

        [Authorize]
        [HttpDelete]
        [Route("/post/delete")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var postName = await _channelService.DeletePost(id);
            return Ok(postName);
        }
        private Guid UserId => Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
