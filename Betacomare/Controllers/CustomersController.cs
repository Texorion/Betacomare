using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomare.ModelsBetacomare;
using Betacomare.Authentication;

namespace Betacomare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BetacomareContext _context;

        public CustomersController(BetacomareContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [BasicAuthorization]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            return await _context.Customers.ToListAsync(); // ritorna la lista di tutti i customer
        }

        // GET: api/Customers/mario.rossi@email.com
        [BasicAuthorization]
        [HttpGet("{username}")]
        public async Task<ActionResult<Customer>> GetCustomer(string username)
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customer = await _context.Customers.FindAsync(username);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        [BasicAuthorization]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string username, Customer customer)
        {
            if (username != customer.Username)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(username))
                {
                    return NotFound(); // l'utente non esiste, non e' possibile effettuarvi modifiche
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers/InsertCustomer
        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> InsertCustomer(Customer customer)
        {
          if (_context.Customers == null)
          {
                return BadRequest(); // context non funziona. Risorsa non trovata. Errore backend.
            }

            _context.Customers.Add(customer); // agginge il customer
            
            try
            {
                if (CustomerExists(customer.Username))
                {
                    return Conflict(); // utente, con tale username, gia' registrato (presente sul server).
                }
                else
                {
                    await _context.SaveChangesAsync(); // il context inserisce il customer sul server
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCustomer", new { username = customer.Username }, customer);
        }

        // DELETE: api/Customers/mario.rossi@email.com
        [BasicAuthorization]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteCustomer(string username)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(username);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool CustomerExists(string username)
        {
            return (_context.Customers?.Any(e => e.Username == username)).GetValueOrDefault();
        }
    }
}
