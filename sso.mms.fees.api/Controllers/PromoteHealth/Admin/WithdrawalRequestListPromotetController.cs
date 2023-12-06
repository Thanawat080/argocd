using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Admin
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/admin")]
    public class WithdrawalRequestListPromotetController : ControllerBase
    {

        private readonly IWithdrawalRequestListPromoteAdminServices adminServices;

        // GET: api/<AdminController>

        public WithdrawalRequestListPromotetController(IWithdrawalRequestListPromoteAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }


        [HttpGet("GetAllPersonForAI")]
        public async Task<PersonForAi> GetAllPersonForAI(string withdrawNo, string hosCode)
        {
            try
            {
                var result = await adminServices.GetAllPersonForAI(withdrawNo, hosCode);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpGet("GetAllHosForAI")]
        public async Task<HospitalAi> GetAllHosForAI(string withdrawno)
        {
            try
            {
                var result = await adminServices.GetAllHosForAI(withdrawno);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpGet("getWithdrawalForAi")]
        public async Task<ResponseList<AaiHealthWithdrawalHViewForAi>> GetAllForAI()
        {
            try
            {
                var result = await adminServices.GetAllForAI();
                if (result != null)
                {
                    return new ResponseList<AaiHealthWithdrawalHViewForAi>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "sucess"
                    };

                }
                return new ResponseList<AaiHealthWithdrawalHViewForAi>
                {
                    ResultList = null,
                    Status = 400,
                    Message = "Not Found Result"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseList<AaiHealthWithdrawalHViewForAi>
                {
                    ResultList = null,
                    Status = 400,
                    Message = ex.ToString()
                };

            }
        }
        [HttpGet("getByCheckId/{CheckupId}")]
        public async Task<Response<AaiHealthCheckupH>> GetByCheckId(int CheckupId)
        {
            try
            {
                var result = await adminServices.GetByCheckId(CheckupId);
                if (result != null)
                {
                    return new Response<AaiHealthCheckupH>
                    {
                        Result = result,
                        Status = 200,
                        Message = "sucess"
                    };

                }
                return new Response<AaiHealthCheckupH>
                {
                    Result = null,
                    Status = 400,
                    Message = "Not Found Result"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Response<AaiHealthCheckupH>
                {
                    Result = null,
                    Status = 400,
                    Message = ex.ToString()
                };

            }
        }
    }
}
