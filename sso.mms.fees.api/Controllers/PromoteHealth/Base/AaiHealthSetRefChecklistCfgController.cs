using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/base")]
    public class AaiHealthSetRefChecklistCfgController : ControllerBase
    {
        private readonly IAaiHealthSetRefChecklistCfgBaseServices baseServices;
        public AaiHealthSetRefChecklistCfgController(IAaiHealthSetRefChecklistCfgBaseServices baseServices)
        {
            this.baseServices = baseServices;
        }

        
        [HttpPost("create")]
        public async Task<string> Create(List<AaiHealthSetRefChecklistCfgView> data)
        {
            var result = await baseServices.Create(data);

            return result;
        }

        [HttpPost("update")]
        public async Task<string> Update(List<AaiHealthSetRefChecklistCfgView> data)
        {
            var result = await baseServices.Update(data);

            return result;
        }

    }
}
