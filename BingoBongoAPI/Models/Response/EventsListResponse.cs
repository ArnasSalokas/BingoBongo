using BingoBongoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Response
{
    public class EventsListResponse
    {
        public IEnumerable<EventResponse> MyEvents { get; set; }
        public IEnumerable<EventResponse> OtherEvents { get; set; }
    }
}
