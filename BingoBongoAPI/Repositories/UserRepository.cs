using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;

namespace BingoBongoAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
