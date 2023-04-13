using Microsoft.AspNetCore.Identity;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace rendszerfejlesztes_beadando.BusinessLogic
{
    public class AuthBusinessLogic
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthBusinessLogic(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // Ellenörzi, hogy a felhasználó szerepel-e az adatbázisban és ha igen
        // akkor visszaadja a jogosultságát, ha pedig nem akkor egy üres objektumot ad vissza
        public async Task<UserRole> Login(LoginUser user)
        {
            var authUser = await userManager.FindByNameAsync(user.username);

            if (authUser == null)
            {
                return new UserRole();
            }

            var result = await userManager.CheckPasswordAsync(authUser, user.password);

            if (result)
            {

                var userRole = await userManager.GetRolesAsync(authUser);

                var role = new UserRole
                {
                    Role = userRole[0]
                };

                return role;

                // JWT tokenes autentikáció amit jelenleg nem használunk

                //var authClaims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, authUser.UserName),
                //    // new Claim("emailConfirmed", authUser.EmailConfirmed.ToString())
                //};
                //foreach (var role in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, role));
                //}

                //var token = tokenService.GenerateToken(authClaims);
                //return (new JwtSecurityTokenHandler().WriteToken(token));
            }
            return new UserRole();
        }
    }
}
