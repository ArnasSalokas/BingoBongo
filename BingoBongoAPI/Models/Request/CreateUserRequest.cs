using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Request
{
    public class CreateUserRequest
    {
        public string SlackId { get; set; } 
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
