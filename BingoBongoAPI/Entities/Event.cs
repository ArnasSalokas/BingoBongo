using BingoBongoAPI.Entities.Interfaces;
using System;

namespace BingoBongoAPI.Entities
{
    public class Event : IEntity
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime Time { get; set; }
		public TimeSpan DeadlineDuration { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Place { get; set; }
		public DateTime Created { get; set; }
		public string ChannelId { get; set; }
        public string CreatorAvatar { get; set; }
    }
}
