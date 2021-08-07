using BingoBongoAPI.Entities.Interfaces;

namespace BingoBongoAPI.Entities
{
    public class UserEvent : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
