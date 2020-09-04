using System.Web.Http;
using Iaea.SG.EQUIS.Frontend.Web.JWT.Filters;

namespace Iaea.SG.EQUIS.Frontend.Web.API.m
{
    public class ValueController : ApiController
    {
        [JwtAuthentication]
        [Authorize]
        public string Get()
        {
            return "value";
        }

        public string Custom()
        {
            return "custom";
        }
    }
}
