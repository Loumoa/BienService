using Bien_LouMoa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bien_LouMoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BienController : ControllerBase
    {

        private readonly ILogger<BienController> _logger;

        public BienController(ILogger<BienController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Bien> GetOne(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}")]
        public ActionResult PostOne(string id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOne(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOne(string id)
        {
            throw new NotImplementedException();
        }
        
    }
}