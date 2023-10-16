using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvcAdventureWorks2019.Models;

namespace WebMvcAdventureWorks2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public PeopleController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            return await _context.People.ToListAsync();
        }

        /* -- Ricerca per BusinessEntityID -- */
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var person = await _context.People.FindAsync(id); // ricerca la chiave di Person (BusinessEntityID)

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        /* -- Ricerca per Firstname + Lastname -- */
        // GET: api/Employees/5
        [HttpGet("Name/{first}/{last}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonName(string first, string last)
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            // ricerca, nella lista di Employee (dal DB) ottenuta con ToListAsync, dove LoginId == login cercato<
            var person = await _context.People.Where(person => person.FirstName == first && person.LastName == last).ToListAsync();

            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return person; // ritorna una IEnumerable<Employee>, ovvero una lista ordinata di impiegati di tipo Employee
            }

        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.People == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.People'  is null.");
          }
            _context.People.Add(person);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPerson", new { id = person.BusinessEntityId }, person);
        }
        /*
        {
            "$id": "",
            "businessEntityId": 292,
            "personType": "EM",
            "nameStyle": false,
            "title": null,
            "firstName": "Marco",
            "middleName": "",
            "lastName": "Ghi",
            "employee": null
        }
        */

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.BusinessEntityId == id)).GetValueOrDefault();
        }
    }
}
