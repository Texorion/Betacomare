using Microsoft.AspNetCore.Mvc; // MVC: Model View Controller
using Microsoft.Data.SqlClient;
using System.Numerics;
using WebApp.BusinessLogic;
using WebApp.Models;
using Lib;

namespace WebApp.Controllers
{
    [ApiController]
    // Route: strada per lanciarlo. Va a cercare la cartella Controller
    [Route("[controller]")] // Percorso di partenza: [controller] == Impiegati
    // un controller deve SEMPRE terminare con la parola Controller
    public class ImpiegatiController : Controller // Impiegati
    {


        // per RITORNARNE tutti gli impiegati nella lista
        [HttpGet("GetEmployees")]
        public List<Impiegato> GetEmployees()
        {
            List<Impiegato> employees = new List<Impiegato>();
            Impiegato.ReadImpiegati(employees);

            return employees; // se reader.HasRows => employess is not null.
        }

        // percorso di chiamata (https://localhost:7182/Impiegati/GetEmployeeById/10)
        // + valore Id che viene passato al metodo GetEmployeeId
        // passaggio parametri: {valore1}/{valore2}/...
        [HttpGet("GetEmployeeById/{matricola}")]
        public Impiegato GetEmployeeById(string matricola)
        {
            if (Impiegato.CheckFieldExist(DbManager.Connection(), matricola, "Matricola"))// true: matricola exist
            {
                return Impiegato.ReadImpiegato(matricola);
            }
            else
            { // else: ritorna matricola vuota
                return new Impiegato();
            }
        }

        [HttpPost("InsertEmployee")]
        public Impiegato InsertEmployee(Impiegato employee)
        {
            // INSERT employee into DB
            Impiegato.InsertImpiegato(employee);

            return employee;
        }

        [HttpPut("UpdateEmployee/{matricola}")]
        public void UpdateEmployee(string matricola, Impiegato employee)
        {
            // UPDATE employee into DB
        }

        [HttpDelete("DeleteEmployee/{matricola}")]
        public void DeleteEmployee(string matricola)
        {
            // DELETE employee into DB
            Impiegato.DeleteImpiegato(matricola);
        }
    }
}
