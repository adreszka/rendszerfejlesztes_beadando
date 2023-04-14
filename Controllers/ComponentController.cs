using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]")]
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

        // Ezt az endpointot hívja meg a raktárvezető ha új alkatrészt akar hozzáadni 
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] ComponentModel component)
        {
            var result = await repo.Add(mapper.Map<Component>(component));
            return Ok(result);

        }

        // Ezt az endpointot hívja meg a raktárvezető ha módosítani akarja az árát az adott alkatrésznek
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] ComponentModel component)
        {
            var result = await repo.Update(component);
            return Ok(result);
        }

        // Ezzel az endpointal le lehet kérni az összes alkatrészt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentModel>>> Get() 
        {
            var result = await repo.GetAll();
            return Ok(result.Select(mapper.Map<ComponentModel>));
        }
    }

}
