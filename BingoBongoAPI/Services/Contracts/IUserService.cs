using BingoBongoAPI.Models.Request;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface IUserService
    {
        public Task CreateUser(CreateUserRequest request);
    }
}
