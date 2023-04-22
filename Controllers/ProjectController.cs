using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
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

        public async Task<ActionResult<bool>> addNewProject([FromBody] NewProject project) 
        {
            var result = await repo.addNewProject(project);
            return Ok(result);
        }
    }
}
