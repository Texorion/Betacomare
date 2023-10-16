using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomare.ModelsBetacomare;
using Betacomare.Authentication;
using Betacomare.Models;

namespace Betacomare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly BetacomareContext _context;

        public AddressesController(BetacomareContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [BasicAuthorization]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelsBetacomare.Address>>> GetAddresses()
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/GetAllAddresses/{username}
        [BasicAuthorization]
        [HttpGet("[action]/{username}")]
        public async Task<ActionResult<IEnumerable<ModelsBetacomare.Address>>> GetAllAddresses(string username)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.Where(u => u.Username == username).ToListAsync();

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // GET: api/Addresses/5
        [BasicAuthorization]
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelsBetacomare.Address>> GetAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        [BasicAuthorization]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, ModelsBetacomare.Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses/InsertAddress
        [HttpPost("[action]")]
        public async Task<ActionResult<ModelsBetacomare.Address>> InsertAddress(ModelsBetacomare.Address address)
        {
            if (_context.Addresses == null)
            {
                return BadRequest(); // context non funziona. Risorsa non trovata. Errore backend.
            }

            _context.Addresses.Add(address);

            try
            {
                if (AddressExists(address.UsernameNavigation.Username))
                {
                    return Conflict(); // utente, con tale address, gia' registrato (presente sul server).
                }
                else
                {
                    await _context.SaveChangesAsync(); // il context inserisce l'address sul server
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [BasicAuthorization]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
        }

        private bool AddressExists(string username)
        {
            return (_context.Addresses?.Any(e => e.UsernameNavigation.Username == username)).GetValueOrDefault();
        }
    }
}
