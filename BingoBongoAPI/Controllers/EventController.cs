using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoBongoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // create event
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] CreateEventRequest request)
        {
            try
            {
                var response = await _eventService.CreateEvent(request);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // update event

        // delete event

        // create channel
    }
}
