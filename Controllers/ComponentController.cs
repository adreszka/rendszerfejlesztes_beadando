using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly ComponentRepository repo;
        private readonly IMapper mapper;

        public ComponentController(ComponentRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ComponentModel component)
        {
            await repo.Add(mapper.Map<Component>(component));
            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(string name, int price)
        {
            await repo.Update(name, price);
            return NoContent();
        }
    }

}
