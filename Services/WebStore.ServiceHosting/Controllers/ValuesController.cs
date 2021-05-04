using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebAPI.TestWebAPI)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i:00}")
            .ToList();

        [HttpGet] // http://localhost:5001/api/values
        public IEnumerable<string> Get() => _values;

        //[HttpGet] // http://localhost:5001/api/values
        //public ActionResult<IEnumerable<string>> Get() => _values;

        [HttpGet("{id}")] // http://localhost:5001/api/values/5
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();

            if (id >= _values.Count)
                return NotFound();

            return _values[id];
        }

        [HttpPost] // post -> http://localhost:5001/api/values
        [HttpPost("add")] // post -> http://localhost:5001/api/values/add
        public ActionResult Post([FromBody] string value)
        {
            _values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = _values.Count - 1 }); // http://localhost:5001/api/values/10
        }

        [HttpPut("{id}")] // put -> http://localhost:5001/api/values/10
        [HttpPut("edit/{id}")] // put -> http://localhost:5001/api/values/edit/10
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _values.Count)
                return NotFound();

            _values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")] // delete -> http://localhost:5001/api/values/10
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _values.Count)
                return NotFound();

            _values.RemoveAt(id);

            return Ok();
        }
    }
}
