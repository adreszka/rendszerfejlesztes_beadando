using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.BusinessLogic;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthBusinessLogic authBl;

        public AuthController(AuthBusinessLogic authBl)
        {
            this.authBl = authBl;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginUser user)
        {

            var result = await authBl.Login(user);
            return Ok(result);
        }
    }
}
