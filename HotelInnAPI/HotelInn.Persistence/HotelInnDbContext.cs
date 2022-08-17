using HotelInn.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelInn.Persistence
{
    public sealed class HotelInnDbContext : DbContext
    {
        public HotelInnDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
    }
}
