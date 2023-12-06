using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.Services;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class ManageBookHealthCheckupController : ControllerBase
    {
        private readonly IManageBookHealthCheckupExtServices extService;

        public ManageBookHealthCheckupController(IManageBookHealthCheckupExtServices extService)
        {
            this.extService = extService;
        }

        [HttpPost("CreateBookCheckup")]
        public async Task<string> CreateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                var result = await extService.CreateBookCheckup(data);

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
        [HttpPost("UpdateBookCheckup")]
        public async Task<string> UpdateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                var result = await extService.UpdateBookCheckup(data);

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
    }
}
