using AutoMapper;
using MatBlazor;
using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.PortalModel;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Admin
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/admin")]
    public class DisbursementHistoryPromoteController : ControllerBase
    {

        private readonly IDisbursementHistoryPromoteAdminServices adminServices;
        public DisbursementHistoryPromoteController(IDisbursementHistoryPromoteAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }
        [HttpGet("[action]")]
        public async Task<ResponseList<PayOrderHistoryView>> GetDisbursementList()
        {
            try
            {
                
                var result =  await adminServices.GetDisbursementList();
                if (result != null)
                {
                    return new ResponseList<PayOrderHistoryView>
                    {
                        ResultList = result,
                        Status = 200,
                        Message = "success"

                    };
                }
                return new ResponseList<PayOrderHistoryView>
                {
                    ResultList = new List<PayOrderHistoryView>(),
                    Status = 400,
                    Message = "empty"

                };
            }
            catch (Exception ex) { 
                return new ResponseList<PayOrderHistoryView>
                {
                    ResultList = new List<PayOrderHistoryView>(),
                    Status = 500,
                    Message = ex.Message

                };
            } 

        }
    }
}
