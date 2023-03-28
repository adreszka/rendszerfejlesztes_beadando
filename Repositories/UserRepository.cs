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
        public async Task Add(User parameters)
        {
            var userFound = authContext.Users.FirstOrDefault(p => p.UserName == parameters.UserName);
            if (userFound != null)
            {
                // jelenleg nem tudom hogyan kéne lekezelni
                throw new Exception();
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
        }
    }
}
