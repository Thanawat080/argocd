using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sso.mms.helper.Data;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.KeyCloak;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadTokenController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IdpDbContext db;

        public ReadTokenController(IdpDbContext db)
        {
            this.db = db;
        }

        [HttpGet]

        public async Task<string> Get()
        {
            return "Success api running";
        }


        // GET: api/<ShortTokenController>


        [HttpGet("[action]/{shortToken}")]
        public async Task<ActionResult<IEnumerable<ResponseShortToken>>> GetToken(string shortToken)
        {
            return await db.SessionUserTs.Where(w => w.ShortToken == shortToken)
              .Select(x =>
              new ResponseShortToken
              {
                  shortToken = x.ShortToken,
                  accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.access_token,
                  expiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.expires_in,
                  refreshExpiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.refresh_expires_in,
                  refreshToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.refresh_token,
                  tokenType = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.token_type,
                  notBeforePolicy = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.notbeforepolicy,
                  sessionState = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.session_state,
                  scope = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.scope,
                  realmGroup = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.realmGroup,
                  
                  // profile
              }).ToListAsync();

        }
    }
}
