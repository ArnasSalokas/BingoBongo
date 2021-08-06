using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BingoBongoContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindBySlackId(string slackId) =>
            await _dbSet.FirstOrDefaultAsync(u => u.SlackUserId == slackId);
    }
}
