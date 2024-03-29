﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Repositories;

namespace rendszerfejlesztes_beadando.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository repo;

        public UserController(UserRepository repo)
        {
            this.repo = repo;
        }

        // Ezzel az endpointal hozzá lehet adni új felhasználót a rendszerbe
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] User user)
        {
            var result = await repo.Add(user);
            return Ok(result);
        }
    }
}
