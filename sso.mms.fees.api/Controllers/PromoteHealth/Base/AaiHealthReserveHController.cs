using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthReserveHController : ControllerBase
    {
        private readonly IAaiHealthReserveHBaseServices baseServices;
        public AaiHealthReserveHController(IAaiHealthReserveHBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        [HttpGet("UpdateReserveStatus")]
        public async Task<string> UpdateReserveStatus(decimal reserveId, string status)
        {
            var result = await baseServices.UpdateReserveStatus(reserveId, status);
            return result;
        }

    }
}
