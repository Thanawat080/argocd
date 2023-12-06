using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Controllers.Dental.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("Dental/Ext")]
    public class HistoryWithdrawalController : ControllerBase
    {
        private readonly IHistoryWithdrawals extServices;
        public HistoryWithdrawalController(IHistoryWithdrawals _extServices)
        {
            this.extServices = _extServices;
        }
        [HttpGet("GetHistorys")]
        public async Task<ResponseList<AaiDentalWithdrawTView>> GetHistorys()
        {
            try
            {
                var result = await extServices.GetHistorys();
                if (result != null)
                {
                    return new ResponseList<AaiDentalWithdrawTView>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "success"
                    };
                }
                return new ResponseList<AaiDentalWithdrawTView>
                {
                    ResultList = null,
                    Status = 400,
                    Message = "NotFound"

                };

            }
            catch (Exception ex)
            {
                return new ResponseList<AaiDentalWithdrawTView>
                {
                    ResultList = null,
                    Status = 500,
                    Message = ex.Message

                };
            }
        }

    }

}
