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

            return CreatedAtAction(nameof(GetChannelAsync),new { channelName = readChannelDTO.Id},readChannelDTO);
        }
        /// <summary>
        /// Returns all Channels in database without searching .
        /// </summary>


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllChannels()
        {
            var channels = await _channelService.GetAllChannelsAsync();
            var channelsDTOs = _mapper.Map<List<Channel>>(channels);
            return Ok(channelsDTOs);
        }

        /// <summary>
        /// Returns specific channel by name.
        /// </summary>
  
        [AllowAnonymous]
        [HttpGet("{channelName}")]
        public async Task<ActionResult> GetChannelAsync(string channelName)
        {
            var channel = await _channelService.GetChannelAsync(channelName);

            if(channel == null)
            {
                return NotFound();
            }

            var readChannelDTO = _mapper.Map<ReadChannelWithoutPostsDTO>(channel);
            return Ok(readChannelDTO);
        }

        /// <summary>
        /// Subscribe to channel.
        /// </summary>
    
        [Authorize]
        [HttpPut("{channelName}")]
        public async Task<ActionResult> SubscribeToChannelAsync(string channelName)
        {
            await _channelService.SubscribeToChannelAsync(UserId, channelName);
            return Ok();
        }

        /// <summary>
        /// Creates post,using json obj.
        /// </summary>
      
        [Authorize]
        [HttpPost("post/")]
        public async Task<IActionResult> CreatePostAsync(CreatePostDTO PostDTO)
        {
           
            var post = _mapper.Map<Post>(PostDTO);
            var createdPost = await _channelService.CreatePostAsync(post,UserId);
            var readPostDTO = _mapper.Map<ReadPostDTO>(createdPost);
            return CreatedAtAction(nameof(GetPostAsync), new { id = readPostDTO.Id }, readPostDTO);

        }
        /// <summary>
        /// Return post by id.
        /// </summary>
    
        [AllowAnonymous]
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetPostAsync(Guid id)
        {
            var post = await _channelService.GetPostByIdAsync(id);
            
            if(post == null)
            { return NotFound(); }

            var ReadPostDto = _mapper.Map<ReadPostWithCommentsDTO>(post);
            return Ok(ReadPostDto);
        }
        /// <summary>
        /// Deletes post.
        /// </summary>

        [Authorize]
        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
             await _channelService.DeletePostAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Likes post when querid once,unlikes on second request.
        /// </summary>

        [Authorize]
        [HttpPut("post/like/{PostId}")]

        public async Task<IActionResult> LikePostAsync(Guid PostId)
        {
            var likeCount = await _channelService.LikePostAsync(PostId, UserId);
            return Ok(likeCount);
        }

        /// <summary>
        /// Creates comment on specific post.
        /// </summary>
      
        [Authorize]
        [HttpPost("post/comment/")]
        public async Task<IActionResult> CommentPostAsync (CommentCreateDTO comment)
        {
            var mapComment = _mapper.Map<Comment>(comment);
            await _channelService.LeaveCommentAsync(mapComment, UserId);

            return Ok();

        }
        /// <summary>
        /// Returns list of posts for channel.
        /// </summary>
       
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
