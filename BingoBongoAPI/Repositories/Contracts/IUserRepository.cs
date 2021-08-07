using BingoBongoAPI.Entities;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> FindBySlackId(string slackId);
    }
}
