using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Boomerang.Api.Features.Classes
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly ICollection<ClassModel> _repository;

        public ClassesController(ICollection<ClassModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return _repository.First().Description;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClassModel model)
        {
            _repository.Add(model);

            return Ok();
        }
    }

    public class ClassModel
    {
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
    }
}
