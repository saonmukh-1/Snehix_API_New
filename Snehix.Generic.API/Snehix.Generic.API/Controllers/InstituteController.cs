﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Snehix.Generic.API.Models;
using Snehix.Generic.API.Services;

namespace Snehix.Generic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {

        public string connString { get; set; }
        public InstituteController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("Default");
        }

        // GET api/Entity
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new InstituteRepositoryService(connString);
            var result = await service.GetAllInstitutes();
            return new ObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var service = new InstituteRepositoryService(connString);
            var result = await service.GetInstituteById(id);
            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InstitutionModel model)
        {
            try
            {
                var service = new InstituteRepositoryService(connString);
                await service.CreateInstitute(model);
                return new ObjectResult("Success");
            }
            catch (Exception ex)
            {
                return new ObjectResult("Faliure: " + ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InstitutionModel model)
        {
            try
            {
                var service = new InstituteRepositoryService(connString);
                await service.UpdateInstitute(model,id);
                return new ObjectResult("Success");
            }
            catch (Exception ex)
            {
                return new ObjectResult("Faliure: " + ex.Message);
            }
        }
    }
}