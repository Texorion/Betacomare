using Betacomare.Models;
using Betacomare.Classes;
using Betacomare.ModelsServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Betacomare.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        #region constructor
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock) { }
        #endregion


        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization")) // ottiene la chiave authorization se qualcuno inserisce delle credenziali di login al popoup di login
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            var authoHeaderRegEx = new Regex("Basic (.*)"); // set nuovo ogg Regex che contine una stringa con Basic + spazio + qualsiasi carattere

            // se Authorization contiene una stringa con basic + spazio + qualsiasi carattere (user e password criptate assieme in automatico), prosegue
            if (!authoHeaderRegEx.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Code, not properly formatted"));
            }

            // decripta, per ogni stringa che rispetta la regex (Basic + spazio + caratteri), la stringa di Authorization (che contiene user + password inserite nel popup d'accesso)
            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authoHeaderRegEx.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2); // divide la stringa per user e password

            // si salva lo user
            var authUser = authSplit[0];
            // verifica che authSplit contenga si auser che password
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get Password");

            if (Encryption.VerifyPwd(GetFromDb(authUser), authPassword))
            {
                // se l'utente esiste:
                // creazione oggetti per l'utente autenticato che creato un ticket d'accesso
                var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, authUser);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail("Utente/Password non corretti! Addio.."));
            }
        }


        #region Connect to DB with SqlConnection
        public static SqlConnection Connection()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var builder = configurationBuilder.Build();

            SqlConnection sqlCnn = new SqlConnection(builder.GetConnectionString("SqlDbCnnToServices"));
            if (sqlCnn.State == ConnectionState.Closed)
            {
                sqlCnn.Open();
            }

            return sqlCnn;
        }
        #endregion


        #region Get salt e pswHash from DB
        public static KeyValuePair<string, string> GetFromDb(string authUser)
        {
            string salt = string.Empty, pswHash = string.Empty;

            SqlCommand cmd = new SqlCommand($"SELECT PswHash, Salt FROM Utenti WHERE username = @user", Connection());
            SqlParameter user = new SqlParameter("@user", authUser);
            cmd.Parameters.Add(user);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    salt = (string)reader["Salt"];
                    pswHash = (string)reader["PswHash"];
                }
            }

            return new KeyValuePair<string, string>(salt, pswHash);
        }
        #endregion
    }
}
