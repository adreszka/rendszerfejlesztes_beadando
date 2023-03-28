using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository repo;

        public UserController(UserRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await repo.Add(user);
            return NoContent();
        }
    }
}
