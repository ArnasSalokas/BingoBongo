using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;

namespace BingoBongoAPI.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(BingoBongoContext dbContext) : base(dbContext)
        {
        }
    }
}
