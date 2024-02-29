using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clementoni1WebAPI.Models.DB;
using MediatR;
using Clementoni1WebAPI.Handlers.QueryHandlers;
using Clementoni1WebAPI.Handlers.CommandHandlers;

namespace Clementoni1WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly FormazioneDBContext _context;

        private readonly IMediator _mediator;

        public PersonController(FormazioneDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;   
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            var result = await _mediator.Send(new GetPersonaConDapperQuery());
            return result;
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var result = await _mediator.Send(new GetPersonaByIdConDapperQuery(id));
            return result;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            await _mediator.Send(new PutPersonaByIdCommand(id, person));

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            await _mediator.Send(new PostPersonaCommand(person));

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            await _mediator.Send(new DeletePersonaByIdCommand(id));

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
