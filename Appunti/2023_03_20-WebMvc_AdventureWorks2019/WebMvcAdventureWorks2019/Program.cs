using WebMvcAdventureWorks2019.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebMvcAdventureWorks2019
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container:
            builder.Services.AddControllers().AddJsonOptions(
                jsonOpt => jsonOpt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<AdventureWorks2019Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection"))); // DB mappato a SQL Server
            /*
             * Console di Gestione pacchetti: Install-Package Microsoft.EntityFrameworkCore.Tools
             *      Crea una classe per la creazione e gestione di una tabella (nel DB creato/da creare settato in appsettings.json) in SQL Server, ovvero per la Migrazione:
             *          add-migration InitDB
             *      Esegue le richieste della migration verso il DB in SQL Server:
             *          update-database
             *      Rimuove l'ultima Migration:
             *          remove-migration
             *      
             *      
             *      Processo inverso, che estrae come classi tutte le tabelle del DB_selezionato:
             *          scaffold-DbContext "Stringa di connessione locale (local host) con Initial Catalog=DB_selezionato" Microsoft.EntityFrameworkCore.SqlServer -Output Cartella_output
             *          Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=AdventureWorks2019;Trusted_Connection=True;TrustServerCertificate=True;" 
             *              Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables ......
             *      Aggiungi poi i Controllers con:
             *          tasto Dx su Controllers -> Aggiungi -> Scaffolding -> Controller API con azioni, che usa Identity Framework
             */

            // quando parti, carica il DbContext, che a sua volta carica in memoria il DB "MyDB" tramite la classe MvcDbContext
            //builder.Services.AddDbContext<MvcDbContext>(opt => opt.UseInMemoryDatabase("DBCars")); // DB in memoria


            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}