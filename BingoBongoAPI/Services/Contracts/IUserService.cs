using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface IUserService
    {
        public Task<UserLoginResponse> CreateUser(CreateUserRequest request);
    }
}
