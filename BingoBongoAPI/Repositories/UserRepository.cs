using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindBySlackId(string slackId) =>
            await _dbSet.FirstOrDefault(u => u.SlackUserId == slackId);
    }
}
