using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services
{
    public class EventService : IEventService
    {
        private readonly ISlackService _slackService;
        public EventService(ISlackService slackService)
        {
            _slackService = slackService;
        }

        public async Task<Event> CreateEvent(CreateEventRequest request)
        {
            // create db event
            // slack api event
        }
        
    }
}
