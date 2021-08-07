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
        private readonly IUserEventRepository _userEventRepository;
        public EventService(ISlackService slackService, IEventRepository eventRepository, IUserRepository userRepository, IUserEventRepository userEventRepository)
        {
            _slackService = slackService;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _userEventRepository = userEventRepository;
        }

        public async Task<EventsListResponse> GetEvents(int userId)
        {
            var allEvents = await _eventRepository.GetEvents();
            var joinedEvents = await _userEventRepository.GetUsersEvents(userId);

            return new EventsListResponse
            {
                MyEvents = allEvents.Where(e => e.UserId == userId).Select(e => new EventResponse(e, true)),
                OtherEvents = allEvents.Where(e => e.UserId != userId).Select(e => new EventResponse(e, joinedEvents.Contains(e.Id)))
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

            var exists = _userEventRepository.UserEventExists(request);

            if (exists)
                return;

            // Check if deadline is not yet expired
            if (DateTime.Now + _event.DeadlineDuration >= _event.Time)
                return;

            await _userEventRepository.Add(new UserEvent
            {
                EventId = request.EventId,
                UserId = request.UserId,
            });

            _slackService.JoinEvent(_event.ChannelId, user.SlackUserId);
        }
    }
}
