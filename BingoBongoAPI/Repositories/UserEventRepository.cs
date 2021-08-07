using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public class UserEventRepository : BaseRepository<UserEvent>, IUserEventRepository
    {
        public UserEventRepository(BingoBongoContext dbContext) : base(dbContext)
        {
        }

        public bool UserEventExists(JoinEventRequest request)
        {
            var entity = _dbSet.FirstOrDefault(ue => ue.UserId == request.UserId && ue.EventId == request.EventId);

            return entity == null ? false : true;
        }
        public async Task<IEnumerable<int>> GetUsersEvents(int userId) =>
            await _dbSet.Where(ue => ue.UserId == userId).Select(ue => ue.EventId).ToListAsync();
    }
}
