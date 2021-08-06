﻿using System;

namespace BingoBongoAPI.Entities
{
    public class Event
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime Time { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Place { get; set; }
		public DateTime Created { get; set; }
	}
}
