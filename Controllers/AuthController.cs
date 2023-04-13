using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.BusinessLogic;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthBusinessLogic authBl;

        public AuthController(AuthBusinessLogic authBl)
        {
            this.authBl = authBl;
        }

        // Ezt az endpointot hívja meg a felhasználó amikor rányom a bejelentkezés gombra
        // és visszaadja a felhasználó jogát amit kap az AuthBusinessLogic Login funkciójától
        [HttpPost]
        public async Task<ActionResult<UserRole>> Login([FromBody] LoginUser user)
        {
            var result = await authBl.Login(user);
            return Ok(result);
        }
    }
}
