using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models.Entities;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository repo;

        public ProjectController(ProjectRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddNewProject([FromBody] NewProject project)
        {
            var result = await repo.addNewProject(project);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddWorkTimeAndFee([FromBody] WorkTimeAndFee timeandfee)
        {
            var results = await repo.AddWorkTimeAndFee(timeandfee);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddComponentToProject([FromBody] AddComponentToProject component)
        {
            var result = await repo.AddComponentToProject(component);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var result = await repo.GetProjects();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectComponent>>> GetProjectComponents()
        {
            var result = await repo.GetProjectComponents();
            return Ok(result);
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<ProjectStatus>> GetProjectsWithStatus(string location)
        {
            var result = await repo.GetProjectsWithStatus(location);
            return Ok(result);
        }
    }
}
