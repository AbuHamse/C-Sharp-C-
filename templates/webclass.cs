using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyWebClass : ControllerBase
    {
        // GET: api/mywebclass
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hello, World!" });
        }

        // GET: api/mywebclass/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(new { Id = id, Message = "Item found" });
        }

        // POST: api/mywebclass
        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            return Created("api/mywebclass", data);
        }

        // PUT: api/mywebclass/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] object data)
        {
            return Ok(new { Id = id, UpdatedData = data });
        }

        // DELETE: api/mywebclass/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(new { Id = id, Message = "Item deleted" });
        }
    }
}
