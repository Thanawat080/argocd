using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;

namespace sso.mms.fees.api.Controllers.Dental.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("Dental/Ext")]
    public class ListWithdrawalsController : ControllerBase
    {
        private readonly IListWithdrawals extService; 
        public ListWithdrawalsController(IListWithdrawals extService)
        {
            this.extService = extService;
        }
        [HttpGet("GetListWithdrawals")]
        public async Task<List<AaiDentalCheckH>> GetListWithdrawals()
        {
            try
            {
                List<AaiDentalCheckH> result = await extService.GetListWithdrawals();
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
