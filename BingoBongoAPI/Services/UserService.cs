using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResponse> CreateUser(CreateUserRequest request)
        {
            var user = _userRepository.FindBySlackId(request.SlackId);

            // Add User to DB for the first time, skip other times
            if (user != null)
                return new UserLoginResponse()
                {
                    Id = user.Id
                };

            var newUser = new User
            {
                Created = DateTime.Now,
                Email = request.Email,
                SlackUserId = request.SlackId,
                Name = request.Name,
                Picture = request.Avatar,
            };

            await _userRepository.Add(newUser);

            return new UserLoginResponse()
            {
                Id = newUser.Id
            };
        }
    }
}
