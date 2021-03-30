using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Api.DTOs.Channel;
using Shukhlyada.Api.DTOs.Comment;
using Shukhlyada.Api.DTOs.Post;
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

        /// <summary>
        /// Creates a Channel.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Channel
        ///     {
        ///        "id": "name",
        ///        "description": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="channel"></param>
        /// <returns>A newly created Channel</returns>
        /// <response code="201">Returns the newly created channel</response>
        /// <response code="400">If the channel is already exists</response>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateChannelAsync(CreateChannelDTO createChannelDTO)
        {
            var channel = _mapper.Map<Channel>(createChannelDTO);
            var createdChannel = await _channelService.CreateChannelAsync(channel, UserId);
            var readChannelDTO = _mapper.Map<ReadChannelWithoutPostsDTO>(createdChannel);

            return CreatedAtAction(nameof(GetChannelByNameAsync),new { channelName = readChannelDTO.Id},readChannelDTO);
        }

        [AllowAnonymous]
        [HttpGet("{channelName}")]
        public async Task<ActionResult> GetChannelByNameAsync(string channelName)
        {
            var channel = await _channelService.GetChannelAsync(channelName);

            if(channel == null)
            {
                return NotFound();
            }

            var readChannelDTO = _mapper.Map<ReadChannelWithoutPostsDTO>(channel);
            return Ok(readChannelDTO);
        }
        [Authorize]
        [HttpPost("post/")]
        public async Task<IActionResult> CreatePostAsync(CreatePostDTO PostDTO)
        {
           
            var post = _mapper.Map<Post>(PostDTO);
            var createdPost = await _channelService.CreatePostAsync(post,UserId);
            var readPostDTO = _mapper.Map<ReadPostDTO>(createdPost);
            return Ok(readPostDTO); // незнаю чи варто шось повертити крім екшнрезалту, але хай буде для тесту

        }
        [AllowAnonymous]
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetPostAsync(Guid id)
        {
            var post = await _channelService.GetPostByIdAsync(id);
            
            if(post == null)
            { return NotFound(); }

            var ReadPostDto = _mapper.Map<ReadPostDTO>(post);
            return Ok(ReadPostDto);
        }

        [Authorize]
        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
             await _channelService.DeletePost(id);
            return NoContent();
        }

        /// 
     
        [Authorize]
        [HttpPost("post/like/{PostId}")]

        public async Task<IActionResult> LikePostAsync(Guid PostId)
        {
            var likeCount = await _channelService.LikePost(PostId, UserId);
            return Ok(likeCount);
        }


        [Authorize]
        [HttpPost("post/comment/")]
        public async Task<IActionResult> CommentPostAsync (CommentCreateDTO comment)
        {
            var mapComment = _mapper.Map<Comment>(comment);
            await _channelService.LeaveComment(mapComment, UserId);

            return Ok();

        }

        [AllowAnonymous]
        [HttpGet("{channelName}/post")]
        public async Task<IActionResult> GetPostsForChannelAsync (string channelName)
        {
            var channel = await _channelService.GetChannelWithPostsAsync(channelName);
            var channelDTO = _mapper.Map<ReadChannelWithPostsDTO>(channel);
            
            return Ok(channelDTO);

        }
        private Guid UserId => Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
