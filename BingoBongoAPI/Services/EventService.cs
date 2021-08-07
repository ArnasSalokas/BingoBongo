using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Services.Contracts;
using BingoBongoAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services
{
    public class EventService : IEventService
    {
        private readonly ISlackService _slackService;
        private readonly IEventRepository _eventRepository;
        public EventService(ISlackService slackService, IEventRepository eventRepository)
        {
            _slackService = slackService;
            _eventRepository = eventRepository;
        }

        public async Task<Event> CreateEvent(CreateEventRequest request)
        {
            // create db event
            var newEvent = new Event()
            {
                Created = DateTime.Now,
                Description = request.Description,
                Name = request.Name,
                Place = request.Place,
                Time = request.Time,
                UserId = request.UserId
            };

            var latestEvent = await _eventRepository.GetByName(request.Name);

            // check if has same name
            if (latestEvent != null)
                newEvent.Name = latestEvent.Name + StringUtils.GetRandomWord();

            await _eventRepository.Add(newEvent);

            // slack api event
            await _slackService.CreateEvent(newEvent);

            return newEvent;
        }
        
    }
}
