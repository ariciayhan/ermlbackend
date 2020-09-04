using Iaea.SG.EQUIS.Frontend.Web.JWT;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace Iaea.SG.EQUIS.Frontend.Web.API.m
{
    public class TokenController : ApiController
    {
        // THis is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        public string Get(string username, string password)
        {
            if (CheckUser(username, password))
            {
                return JwtManager.GenerateToken(new[] { 
                    new Claim("username", username),
                    new Claim("FullName", username),
                    new Claim("LoginName", username),
                    new Claim("EmployeeId", username),
                    new Claim("Endpoint", username)}
                );
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        public bool CheckUser(string username, string password)
        {
            // should check in the database
            return true;
        }
    }
}
