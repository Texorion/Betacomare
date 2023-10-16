using System.Security.Principal;

// **** CODICE AUTHENTICAZIONE ****
namespace Betacomare.Authentication
{
    public class AuthenticatedUser : IIdentity
    {
        public string? AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }

        // oggetto per l'autenticazione
        public AuthenticatedUser(string authType, bool isAuthent, string userName)
        {
            AuthenticationType = authType;
            IsAuthenticated = isAuthent;
            Name = userName;
        }

    }
}

