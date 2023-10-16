using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class PersonEmployeesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public PersonEmployeesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/PersonEmployees
        [HttpGet("{LoginID}")]
        public async Task<ActionResult<IEnumerable<PersonEmployee>>> GetEmployees(string LoginID)
        {

            var res = await _context.PersonEmployees.FromSql($"SELECT P.BusinessEntityID, FirstName, MiddleName, LastName, LoginID, JobTitle, BirthDate FROM HumanResources.Employee AS E JOIN Person.Person AS P ON E.BusinessEntityID = P.BusinessEntityID WHERE LoginID LIKE '%{LoginID}'").ToListAsync();

            //var employees = await _context.Employees.Where(w => w.LoginId == "adventure-works\\" + LoginID).ToListAsync();
            //var people = await _context.People.ToListAsync();

            //var res = from p in people
            //          join e in employees on p.BusinessEntityId equals e.BusinessEntityId
            //          select new PersonEmployee
            //          {
            //              BusinessEntityId = p.BusinessEntityId,
            //              FirstName = p.FirstName,
            //              MiddleName = p.MiddleName,
            //              LastName = p.LastName,
            //              LoginID = e.LoginId,
            //              JobTitle = e.JobTitle,
            //              BirthDate = e.BirthDate
            //          };

            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

    }
}
