using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{

    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class GetReserveHController : ControllerBase
    { 
        private readonly IGetReserveHExtServices extService;

        public GetReserveHController(IGetReserveHExtServices extService)
        {
            this.extService = extService;
        }

        [HttpGet("GetAll/{hoscode}")]
        public async Task<List<GetReServeHView>> GetAll(string hoscode)
        {
            List<GetReServeHView> result = await extService.GetAll(hoscode);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetReserveById/{reserveId}")]
        public async Task<ManageBookHealthCheckupView> GetReserveHById(int reserveId)
        {
            var result = await extService.GetReserveHById(reserveId);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
