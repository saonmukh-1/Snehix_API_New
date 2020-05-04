using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Snehix.Generic.API.Services;

namespace Snehix.Generic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string connString { get; set; }
        public ValuesController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("Default");
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = new EntityRepositoryService(connString);
            var result = await service.GetAllEntityType();
            return new ObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
