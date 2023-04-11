using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace rendszerfejlesztes_beadando.Data
{
    public class AuthDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        // Az adatbázisba eltároljuk, hogy milyen jogosultságok vannak és hozzáadjuk az admint a rendszerhez
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var roles = new List<IdentityRole>();
            roles.Add(new IdentityRole { Name = "Administrator", NormalizedName = "Administrator".ToUpper() });
            roles.Add(new IdentityRole { Name = "Specialist", NormalizedName = "Specialist".ToUpper() });
            roles.Add(new IdentityRole { Name = "Warehouseman", NormalizedName = "Warehouseman".ToUpper() });
            roles.Add(new IdentityRole { Name = "WarehouseManager", NormalizedName = "WarehouseManager".ToUpper() });

            var admin = new IdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper()
            };

            var password = new PasswordHasher<IdentityUser>();
            var hashed = password.HashPassword(admin, "admin");
            admin.PasswordHash = hashed;

            builder.Entity<IdentityRole>().HasData(
                roles
            );

            builder.Entity<IdentityUser>().HasData(
                admin
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = admin.Id, RoleId = roles[0].Id }
            );

            base.OnModelCreating(builder);

        }
    }
}
