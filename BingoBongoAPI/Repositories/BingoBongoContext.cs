using BingoBongoAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Repositories
{
    public class BingoBongoContext : DbContext
    {
        public BingoBongoContext(DbContextOptions<BingoBongoContext> options) : base(options)
        {

        }

        public DbSet<User> User{ get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }
    }
}
