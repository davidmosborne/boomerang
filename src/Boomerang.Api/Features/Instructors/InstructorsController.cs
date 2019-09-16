using System;
using System.Collections.Generic;
using System.Linq;

using Boomerang.Api.Models;

using Microsoft.AspNetCore.Mvc;

namespace Boomerang.Api.Features.Instructors
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorsController : Controller
    {
        private readonly ICollection<Instructor> _repository;

        public InstructorsController(ICollection<Instructor> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Instructor model)
        {
            var instructor = new Instructor
            {
                Id = DateTime.Now.Second,
                Name = model?.Name
            };

            _repository.Add(instructor);

            return Created(new Uri($"/instructors/{instructor.Id}", UriKind.Relative), instructor);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var instructor = _repository.SingleOrDefault(i => i.Id == id);

            return instructor != null ? Ok(instructor) : NotFound() as IActionResult;
        }
    }
}
