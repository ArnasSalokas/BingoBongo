using BingoBongoAPI.Entities.Enums;
using BingoBongoAPI.Entities.Interfaces;
using System;

namespace BingoBongoAPI.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string SlackUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public DateTime Created { get; set; }
        public UserType Type { get; set; }
    }
}
