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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AuthTokenService tokenService;
        private readonly AuthDbContext authContext;
        public AuthBusinessLogic(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AuthTokenService tokenService, AuthDbContext authContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.authContext = authContext;
        }

        public async Task<string> Login(LoginUser user)
        {
            var authUser = await userManager.FindByNameAsync(user.username);

            if (authUser == null)
            {
                // valamit dobni kell exceptionnek
                // throw new ResourceNotFoundException();
                return "";
            }

            var result = await userManager.CheckPasswordAsync(authUser, user.password);

            if (result)
            {

                var userRoles = await userManager.GetRolesAsync(authUser);



                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authUser.UserName),
                    // new Claim("emailConfirmed", authUser.EmailConfirmed.ToString())
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = tokenService.GenerateToken(authClaims);
                return (new JwtSecurityTokenHandler().WriteToken(token));
            }
            // valami hibát dobni kell majd
            // throw new UnauthorizedException();
            return "";
        }
    }
}
