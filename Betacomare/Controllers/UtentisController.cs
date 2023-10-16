using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomare.ModelsServices;
using Betacomare.Classes;
using Betacomare.Authentication;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Betacomare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentisController : ControllerBase
    {
        private readonly AdventureWorksLt2019servicesContext _context;

        public UtentisController(AdventureWorksLt2019servicesContext context)
        {
            _context = context;
        }

        // GET: api/Utentis
        [BasicAuthorization]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUtentis()
        {
          if (_context.Utentis == null)
          {
              return NotFound();
          }
            return await _context.Utentis.ToListAsync();
        }


        // GET: api/Utentis/5
        [BasicAuthorization]
        [HttpGet("{id}")]
        public async Task<ActionResult<Utenti>> GetUtenti(string username)
        {
          if (_context.Utentis == null)
          {
              return NotFound();
          }
            var utenti = await _context.Utentis.FindAsync(username);

            if (utenti == null)
            {
                return NotFound();
            }

            return utenti;
        }

        // Re-search the user just registrated for resed to the output
        // GET: api/Utentis
        [BasicAuthorization]
        [HttpGet()]
        public async Task<ActionResult<Utenti>> GetUsers(string username)
        {
            if (_context.Utentis == null)
            {
                return NotFound();
            }
            var utenti = await _context.Utentis.FindAsync(username);

            if (utenti == null)
            {
                return NotFound();
            }

            return utenti;
        }


        // PUT: api/Utentis/5
        [BasicAuthorization]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtenti(string id, Utenti utenti)
        {
            if (id != utenti.Username)
            {
                return BadRequest();
            }

            _context.Entry(utenti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtentiExists(id))
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


        // POST: api/Utentis/Registra
        [HttpPost("[action]")]
        public async Task<ActionResult<Utenti>> Registra(Utenti utenti)
        {
            if (_context.Utentis == null)
            {
                return BadRequest(); // context non funziona. Risorsa non trovata. Errore backend.
            }

            KeyValuePair<string, string> passwordHash = Encryption.PaswordToHash(utenti.PswHash);
            utenti.PswHash = passwordHash.Key;
            utenti.Salt = passwordHash.Value;

            _context.Utentis.Add(utenti);
            try
            {
                if (UtentiExistsUser(utenti.Username)) //database nuovo ADVServices
                {
                    return Conflict();
                }
                else
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            // make a new object with username = utenti.Username and get its with GetUtenti
            // that return an utenti object with the reserched username
            return CreatedAtAction("GetUsers", new { username = utenti.Username }, utenti);
        }


        // POST: api/Utentis/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(Credential credential)
        {
            // collegamento al DB Utenti non riuscito
            if (_context.Utentis == null)
            {
                return BadRequest(); // context non funziona. Risorsa non trovata. Errore backend.
            }

            // cerco corrispondenza utente per email
            var utenti = await _context.Utentis.FindAsync(credential.user);

            // utente non trovato
            if (utenti == null)
            {
                return Unauthorized(); // utente non trovato, quindi non autorizzato
            }
            else // corrispondenza trovata, check credenziali
            {
                if (Encryption.VerifyPwd(BasicAuthenticationHandler.GetFromDb(credential.user), credential.psw))
                {
                    // se l'utente esiste:
                    // creazione oggetti per l'utente autenticato che creato un ticket d'accesso

                    return Ok();

                    //var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, user);
                    //var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));
                    //return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
                else
                {
                    return Unauthorized(); // utente con credenziali errate, quindi non autorizzato
                }
            }
        }


        // DELETE: api/Utentis/mario@rossi.com
        [BasicAuthorization]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUtenti(string username)
        {
            if (_context.Utentis == null)
            {
                return NotFound();
            }
            var utenti = await _context.Utentis.FindAsync(username);
            if (utenti == null)
            {
                return NotFound();
            }

            _context.Utentis.Remove(utenti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtentiExists(string username)
        {
            return (_context.Utentis?.Any(e => e.Username == username)).GetValueOrDefault();
        }

        private bool UtentiExistsUser(string username)
        {
            return (_context.Utentis?.Any(e => e.Username == username)).GetValueOrDefault();
        }
    }
}
