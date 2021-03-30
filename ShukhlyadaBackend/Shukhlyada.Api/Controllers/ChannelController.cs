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
        [HttpPost("post/")]
        public async Task<IActionResult> CreatePost(CreatePostDTO PostDTO)
        {
           
            var post = _mapper.Map<Post>(PostDTO);
            var createdPost = await _channelService.CreatePostAsync(post,UserId);
            var readPostDTO = _mapper.Map<ReadPostDTO>(createdPost);
            return Ok(readPostDTO); // незнаю чи варто шось повертити крім екшнрезалту, але хай буде для тесту

        }
        [Authorize]
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _channelService.GetPostByIdAsync(id);
            
            if(post == null)
            { return NotFound(); }

            var ReadPostDto = _mapper.Map<ReadPostDTO>(post);
            return Ok(ReadPostDto);
        }

        [Authorize]
        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
             await _channelService.DeletePost(id);
            return NoContent();
        }

        /// 
     
        [Authorize]
        [HttpPost("post/like/{PostId}")]

        public async Task<IActionResult> LikePost(Guid PostId)
        {
            var likeCount = await _channelService.LikePost(PostId, UserId);
            return Ok(likeCount);
        }


        [Authorize]
        [HttpPost("post/comment/")]
        public async Task<IActionResult> CommentPost (CommentCreateDTO comment)
        {
            var mapComment = _mapper.Map<Comment>(comment);
            await _channelService.LeaveComment(mapComment, UserId);

            return Ok();

        }

        [AllowAnonymous]
        [HttpGet("{ChannelId}/post")]
        public async Task<IActionResult> GetPostsForChannel (Guid ChannelId)
        {
            var postList = await _channelService.GetAllPostsForChannel(ChannelId);
            var mappedPostList = new List<ReadPostDTO>();
            
            foreach(var a in postList)
            {
                mappedPostList.Add(_mapper.Map<ReadPostDTO>(a));
            }
            return Ok(mappedPostList);

        }
        private Guid UserId => Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
