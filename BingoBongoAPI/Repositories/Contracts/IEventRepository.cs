using BingoBongoAPI.Entities;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories.Contracts
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        public Task<Event> GetByName(string name);
    }
}
