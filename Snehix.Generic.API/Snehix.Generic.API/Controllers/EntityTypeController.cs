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
    public class EntityTypeController : ControllerBase
    {
        public string connString { get; set; }
        public EntityTypeController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("Default");
        }

        // GET api/Entity
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new EntityRepositoryService(connString);
            var result = await service.GetAllEntityType();
            return new ObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EntityTypeModel model)





        {
            try
            {
                var service = new EntityRepositoryService(connString);
                await service.CreateEntityType(model.Name, model.Description);
                return new ObjectResult("Success");
            }
            catch (Exception ex)
            {
                return new ObjectResult("Faliure: " + ex.Message);
            }
        }
    }
}