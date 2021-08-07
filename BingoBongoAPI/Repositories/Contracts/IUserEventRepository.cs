using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Contracts
{
    public interface IUserEventRepository : IBaseRepository<UserEvent>
    {
        public bool UserEventExists(JoinEventRequest request);
        public Task<IEnumerable<int>> GetUsersEvents(int userId);
    }
}
