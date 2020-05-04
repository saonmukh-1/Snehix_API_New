using System;
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
    public class EntityController : ControllerBase
    {

        public string connString { get; set; }
        public EntityController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("Default");
        }

        // GET api/Entity
        [HttpGet]
        public async Task<IActionResult> GetByEntityType(int entityTypeId)
        {
            var service = new EntityRepositoryService(connString);
            var result = await service.GetAllEntityByType(entityTypeId);
            return new ObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var service = new EntityRepositoryService(connString);
            var result = await service.GetAllEntityById(id);
            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EntityModel model)
        {
            try
            {
                var service = new EntityRepositoryService(connString);
                await service.CreateEntity(model.Name, model.Description, model.EntityTypeId);
                return new ObjectResult("Success");
            }
            catch(Exception ex)
            {
                return new ObjectResult("Faliure: "+ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EntityModel model)
        {
            try
            {
                var service = new EntityRepositoryService(connString);
                await service.UpdateEntity(id,model.Name, model.Description, model.EntityTypeId);
                return new ObjectResult("Success");
            }
            catch (Exception ex)
            {
                return new ObjectResult("Faliure: " + ex.Message);
            }
        }
    }
}