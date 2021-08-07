using BingoBongoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Response
{
    public class EventResponse : Event
    {
        public IEnumerable<User> Members { get; set; }
    }
}
