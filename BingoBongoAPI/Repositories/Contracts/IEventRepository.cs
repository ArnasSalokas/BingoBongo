using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Contracts
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        public Task<IEnumerable<Event>> GetEvents();
        public Task<Event> GetByName(string name);
    }
}
