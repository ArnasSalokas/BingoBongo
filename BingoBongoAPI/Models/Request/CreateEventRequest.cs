using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Request
{
    public class CreateEventRequest
    {
        public int Id { get; set; } //User Id
        public string Name { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}
