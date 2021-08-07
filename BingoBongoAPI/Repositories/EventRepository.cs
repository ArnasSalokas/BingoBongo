using BingoBongoAPI.Entities;
using BingoBongoAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(BingoBongoContext dbContext) : base(dbContext)
        {
        }

        public async Task<Event> GetByName(string name) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Name == name);
    }
}
