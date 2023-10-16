using Betacomare.Authentication;
using Betacomare.Classes;
using Betacomare.Models;
using Betacomare.ModelsBetacomare;
using Betacomare.ModelsServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Betacomare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(jsonOpt =>
                jsonOpt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //preserve
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<AdventureWorksLt2019Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbCnnToAzienda")));
            builder.Services.AddDbContext<AdventureWorksLt2019servicesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbCnnToServices")));
            builder.Services.AddDbContext<BetacomareContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbCnnToBetacomare")));

            /* -- Autenticazione -- */
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", opt => { });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication")
                    .RequireAuthenticatedUser().Build());
            });

            /* -- FrontEnd Policy -- */
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "CORSPolicy", builder =>
                {
                    builder
                       .AllowAnyMethod()                        // defining the allowed HTTP method
                       .AllowAnyHeader()                        // allowing any header to be sent
                       .AllowCredentials()
                       .SetIsOriginAllowed((hosts) => true);    // specifying the allowed origin
                });
            });

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
             *          Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=AdventureWorks2019;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
             *      Aggiungi poi i Controllers con:
             *          tasto Dx su Controllers -> Aggiungi -> Scaffolding -> Controller API con azioni, che usa Identity Framework
             */


            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("CORSPolicy");
            app.MapControllers();
            app.Run();
        }
    }
}