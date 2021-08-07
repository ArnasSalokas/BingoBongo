using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
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
        private readonly IUserRepository _userRepository;
        public EventService(ISlackService slackService, IEventRepository eventRepository, IUserRepository userRepository)
        {
            _slackService = slackService;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<EventsListResponse> GetEvents(int userId)
        {
            var allEvents = await _eventRepository.GetEvents();

            return new EventsListResponse
            {
                MyEvents = allEvents.Where(e => e.UserId == userId),
                OtherEvents = allEvents.Where(e => e.UserId != userId),
            };
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
                newEvent.Name = latestEvent.Name + $"-{StringUtils.GetRandomWord()}";

            // slack api event
            var response = _slackService.CreateChannel(newEvent);

            newEvent.ChannelId = response.Channel.Id;

            await _eventRepository.Add(newEvent);

            return newEvent;
        }

        public async Task JoinEvent(JoinEventRequest request)
        {
            var user = await _userRepository.GetByKey(request.UserId);
            var _event = await _eventRepository.GetByKey(request.EventId);

            _slackService.JoinEvent(_event.ChannelId, user.SlackUserId);
        }
    }
}
