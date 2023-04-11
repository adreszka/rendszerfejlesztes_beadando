using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class UserRepository
    {
        private readonly AuthDbContext authContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserRepository(AuthDbContext authContext, UserManager<IdentityUser> userManager)
        {
            this.authContext = authContext;
            this.userManager = userManager;
        }

        // Hozzáadja az új felhasználót az adatbázisba, ha nem szerepel az a felhasználónév
        // és visszaad egy true-t, ha szerepel akkor pedig egy false-t ad vissza
        // Ha true az azt jelenti, hogy sikerült elmenteni az adatbázisba, ha pedig false
        // akkor pedig, hogy nem sikerült
        public async Task<bool> Add(User parameters)
        {
            var userFound = authContext.Users.FirstOrDefault(p => p.UserName == parameters.UserName);
            if (userFound != null)
            {
                return false;
            }
            var user = new IdentityUser
            {
                UserName = parameters.UserName,
                NormalizedUserName = parameters.UserName.ToUpper()
            };

            var password = new PasswordHasher<IdentityUser>();
            var hashed = password.HashPassword(user, parameters.Password);
            user.PasswordHash = hashed;

            var userStore = new UserStore<IdentityUser>(authContext);
            await userStore.CreateAsync(user);

            await userManager.AddToRoleAsync(user, parameters.Role);
            return true;
        }
    }
}
