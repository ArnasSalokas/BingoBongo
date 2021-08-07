using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;

namespace BingoBongoAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BingoBongoContext dbContext) : base(dbContext)
        {
        }
    }
}
