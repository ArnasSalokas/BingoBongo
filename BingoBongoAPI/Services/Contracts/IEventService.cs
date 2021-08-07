using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface IEventService
    {
        public Task<EventsListResponse> GetEvents(int userId);
        public Task<Event> CreateEvent(CreateEventRequest request);
        public Task JoinEvent(JoinEventRequest request);
    }
}
