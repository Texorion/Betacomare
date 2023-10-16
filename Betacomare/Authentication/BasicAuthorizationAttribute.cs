using Microsoft.AspNetCore.Authorization;

// **** CODICE AUTHENTICAZIONE ****
namespace Betacomare.Authentication
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
