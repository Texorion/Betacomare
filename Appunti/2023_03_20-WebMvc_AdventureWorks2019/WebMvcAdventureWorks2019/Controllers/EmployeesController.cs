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
    public class EmployeesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public EmployeesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees.ToListAsync();
        }

        /* -- Ricerca per BusinessEntityID -- */
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id); // ricerca la chiave di Employee (BusinessEntityID)

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        /* -- Ricerca per LoginID -- */
        // GET: api/Employees/5
        [HttpGet("Login/{LoginID}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeLoginID(string LoginID)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            
            // ricerca, nella lista di Employee (dal DB) ottenuta con ToListAsync, dove LoginId == login cercato<
            var employee = await _context.Employees.Where(w => w.LoginId == "adventure-works\\" + LoginID).ToListAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee; // ritorna una IEnumerable<Employee>, ovvero una lista ordinata di impiegati di tipo Employee
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'AdventureWorks2019Context.Employees'  is null.");
            }
            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { id = employee.BusinessEntityId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.BusinessEntityId == id)).GetValueOrDefault();
        }
    }
}
