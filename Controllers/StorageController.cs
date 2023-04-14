﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<bool>> Put([FromBody] StoreComponent parameters)
        {
            var result = await repo.StoreComponent(parameters);
            return Ok(result);
        }
    }
}