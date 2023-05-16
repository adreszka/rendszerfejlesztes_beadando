using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : Controller
    {
        private readonly StorageRepository repo;

        public StorageController(StorageRepository repo) 
        {
            this.repo = repo;
        }

        // Alkatrész tárolását végzi az endpoint hogy melyik rekeszbe tároljuk
        [HttpPut]
        public async Task<ActionResult<int>> Put([FromBody] StoreComponent parameters)
        {
            var result = await repo.StoreComponent(parameters);
            return Ok(result);
        }

        [HttpGet("{componentName}")]
        public async Task<ActionResult<IEnumerable<StorageModel>>> GetFreeComponents(string componentName) 
        {
            var result = await repo.GetFreeComponents(componentName);
            return Ok(result);
        }
    }
}
