using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PingDong.Newmoon.IdentityServer.Models;

namespace PingDong.Newmoon.IdentityServer.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Have to include both base and inherited class
            builder.Entity<IdentityUser>().ToTable("Users", schema: "users");
            builder.Entity<ApplicationUser>().ToTable("Users", schema: "users");

            builder.Entity<IdentityRole>().ToTable("Roles", schema: "users");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schema: "users");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", schema: "users");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", schema: "users");

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", schema: "users");

            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", schema: "users");

        }
    }
}
