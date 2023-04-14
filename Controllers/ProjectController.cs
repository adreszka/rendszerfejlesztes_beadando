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
    }
}
