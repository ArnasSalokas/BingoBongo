using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
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

        public async Task CreateUser(CreateUserRequest request)
        {
            var user = _userRepository.FindBySlackId(request.Id);

            // Add User to DB for the first time, skip other times
            if (user != null)
                return;

            var newUser = new User
            {
                Created = DateTime.Now,
                Email = request.Email,
                SlackUserId = request.Id,
                Name = request.Name,
                Picture = request.Avatar,
            };

            await _userRepository.Add(newUser);
        }
    }
}
