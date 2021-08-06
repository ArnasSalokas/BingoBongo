using BingoBongoAPI.Models.Request;
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
        public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            try
            {
                await _userService.CreateUser(request);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
