﻿using AutoMapper;
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ComponentModel component)
        {
            await repo.Add(mapper.Map<Component>(component));
            return NoContent();

        }

        [HttpPut]
        public async Task<ActionResult> Put(string name, int price)
        {
            await repo.Update(name, price);
            return NoContent();
        }
    }

}
