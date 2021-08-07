using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface IEventService
    {
        public Task<Event> CreateEvent(CreateEventRequest request);
    }
}
