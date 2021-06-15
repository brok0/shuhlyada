using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Api.DTOs.Account;
using Shukhlyada.Api.DTOs.Channel;
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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<ActionResult> CreateAccountAsync(UserRegisterDTO userDTO)
        {
            var account = _mapper.Map<Account>(userDTO);
            var user = await _userService.CreateAccountAsync(account);
            var userRead = _mapper.Map<UserReadDTO>(user);
            return Ok(userRead);
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<ActionResult> LoginUserAsync(UserLoginDTO userDTO)
        {
            var user = await _userService.AuthenticateAsync(userDTO.Email, userDTO.Password);
            if(user == null)
            {
                return NotFound();
            }

            var userRead = _mapper.Map<UserReadDTO>(user);
            return Ok(userRead);
        }


        /// <summary>
        /// Returns channels that user subscribed on.
        /// </summary>

        [Authorize]
        [HttpGet("subscription")]

        public async Task<IActionResult> GetChannelSubscriptionAsync()
        {
            var channelList = await _userService.GetSubscribedChannelsAsync(UserId);

            var map = _mapper.Map<List<ReadChannelWithoutPostsDTO>>(channelList);
            
            return Ok(map);

        }
        /// <summary>
        /// Returns posts that user created.
        /// </summary>
        /// 

        [Authorize]
        [HttpGet("posts/created")]
        public async Task<IActionResult> GetPostsCreatedByUserAsync() {

            var postList = await _userService.GetCreatedPostsAsync(UserId);
            var map = _mapper.Map<List<ReadPostDTO>>(postList);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("posts/liked")]
        public async Task<IActionResult> GetPostsLikedByUser()
        {

            var postList = await _userService.GetLikedPostsAsync(UserId);
            var map = _mapper.Map<List<ReadPostDTO>>(postList);
            return Ok(map);
        }

        [Authorize]
        [HttpPut("user/change-avatar/{imageId}")]
        public async Task<IActionResult> ChangeAvatar(int imageId)
        {
            await _userService.ChangeAvatar(UserId, imageId);
            return Ok();
        }
        private Guid UserId => Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
