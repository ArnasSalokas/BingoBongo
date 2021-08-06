using Microsoft.EntityFrameworkCore;

namespace BingoBongoAPI.Entities
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
    }
}
