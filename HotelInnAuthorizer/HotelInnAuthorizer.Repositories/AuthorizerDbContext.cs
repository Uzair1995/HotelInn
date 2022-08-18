using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HotelInnAuthorizer.Repositories
{
    public class AuthorizerDbContext : IdentityDbContext
    {
        public AuthorizerDbContext(DbContextOptions<AuthorizerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
        }
    }
}
