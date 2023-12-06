using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Collections.Generic;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class GetViewPayOrderController : ControllerBase
    {
        private readonly IGetViewPayOrderExtServices extService;

        public GetViewPayOrderController(IGetViewPayOrderExtServices extService)
        {
            this.extService = extService;
        }

        [HttpGet("GetByHoscode/{hoscode}")]
        public async Task<List<PayOrderView>> GetByHoscode(string hoscode)
        {
            List<PayOrderView> result = await extService.GetByHoscode(hoscode);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        //[HttpGet("GetCheckupHInPayOrder")]
        //public async Task<List<ViewAaiHealthCheckupH>> GetCheckupHInPayOrder(string hoscode, string withdrawalNo)
        //{
        //    List<ViewAaiHealthCheckupH > result = await extService.GetCheckupHInPayOrder(hoscode, withdrawalNo);

        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
