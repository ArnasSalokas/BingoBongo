using BingoBongoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Response
{
    public class EventResponse : Event
    {
        public bool Joined { get; set; }

        public EventResponse(Event e, bool joined)
        {
            Id = e.Id;
            UserId = e.UserId;
            Time = e.Time;
            Name = e.Name;
            Description = e.Description;
            Place = e.Place;
            Created = e.Created;
            ChannelId = e.ChannelId;
            Joined = joined;
        }
    }
}
