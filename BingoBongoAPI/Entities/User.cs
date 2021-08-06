using System;

namespace BingoBongoAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string SlackUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public DateTime Created { get; set; }
    }
}
