using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoBongoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserLoginResponse>> RegisterUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = await _userService.CreateUser(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
