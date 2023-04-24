using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository repo;

        public ProjectController(ProjectRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<ProjectStatus>> GetProjectsWithStatus(string location)
        {
            var result = await repo.GetProjectsWithStatus(location);
            return Ok(result);
        }
    }
}
