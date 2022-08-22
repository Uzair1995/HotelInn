using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HotelInnAuthorizer.Repositories.Models;

namespace HotelInnAuthorizer.Repositories
{
    public class AuthorizerDbContext : IdentityDbContext<User>
    {
        public AuthorizerDbContext(DbContextOptions<AuthorizerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
