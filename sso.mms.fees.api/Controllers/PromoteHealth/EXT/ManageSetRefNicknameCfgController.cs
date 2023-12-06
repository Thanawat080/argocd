using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/ext")]
    public class ManageSetRefNicknameCfgController : ControllerBase
    {
        private readonly IManageSetRefNicknameCfgExt extServices;

        // GET: api/<AdminController>

        public ManageSetRefNicknameCfgController(IManageSetRefNicknameCfgExt extServices)
        {
            this.extServices = extServices;
        }

        [HttpPost("GetSetRefNicknameCfg")]
        public async Task<List<AaiHealthSetRefNicknameCfg>> GetSetRefNicknameCfg(ManageSetRefNicknameCfgView data)
        {
            try
            {

                List<AaiHealthSetRefNicknameCfg> result = await extServices.GetSetRefNicknameCfg(data);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpPost("Update")]
        public async Task<string> Update(List<AaiHealthSetRefNicknameCfg> cfg)
        {
            var result = await extServices.Update(cfg);

            return result;
        }
    }
}
