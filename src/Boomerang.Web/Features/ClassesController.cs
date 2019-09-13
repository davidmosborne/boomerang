using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Boomerang.Web.Features
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ICollection<ClassModel> _repository;

        public ClassesController(ICollection<ClassModel> repository)
        {
            _repository = repository;
        }

        // GET: Classes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _repository.Select(c => c.Description);
        }

        // POST: Classes
        [HttpPost]
        public IActionResult Post([FromBody] ClassModel @class)
        {
            _repository.Add(@class);

            return Ok();
        }
    }

    public class ClassModel
    {
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
    }
}
