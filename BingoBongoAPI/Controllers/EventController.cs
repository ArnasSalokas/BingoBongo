﻿using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
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

        // get events
        [HttpGet("{userId}")]
        public async Task<ActionResult<EventsListResponse>> GetEvents([FromRoute] int userId)
        {
            try
            {
                var response = await _eventService.GetEvents(userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // TODO: update event
        
        // TODO: delete event

        // join event
        [HttpPost("join")]
        public async Task<ActionResult> JoinEvent([FromBody] JoinEventRequest request)
        {
            try
            {
                await _eventService.JoinEvent(request);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
