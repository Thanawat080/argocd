using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/ext")]
    public class SaveDetermineReferenceValueController : ControllerBase
    {
        private readonly ISaveDetermineReferenceValueExt extService;

        public SaveDetermineReferenceValueController(ISaveDetermineReferenceValueExt extService)
        {
            this.extService = extService;
        }

        [HttpPost("Saveandupdate")]
        public async Task<string> saveandupdate(SaveDetermineReferenceValue data)
        {
            try
            {
                var result = await extService.saveandupdate(data);

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

        [HttpPost("saveandupdateDoctor")]
        public async Task<string> saveandupdateDoctor(DataSaveDoctor data)
        {
            try
            {
                var result = await extService.saveandupdateDoctor(data);

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
                return ex.Message;
            }
        }
    }
}
