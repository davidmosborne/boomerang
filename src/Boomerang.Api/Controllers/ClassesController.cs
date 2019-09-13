using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Boomerang.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[]
            {
                "value1",
                "value2"
            };
        }
    }
}
