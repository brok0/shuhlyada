using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shukhlyada.Api.DTOs;
using Shukhlyada.Api.DTOs.Account;
using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
