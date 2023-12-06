using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Controllers.PromoteHealth.Admin
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/admin")]
    public class ManageCallBackController : ControllerBase
    {
        private readonly IManageCallBackService manageCallBackService;

        // GET: api/<AdminController>

        public ManageCallBackController(IManageCallBackService manageCallBackService)
        {
            this.manageCallBackService = manageCallBackService;
        }

        [HttpGet("CallBackPromoteHealth")]
        public async Task<Response<string>> CallBackPromoteHealth([FromQuery(Name = "Fail")] string Fail,
              [FromQuery(Name = "SignBy")] string SignBy, [FromQuery(Name = "PayOrderId")] string PayOrderId)
        {
            Response<string> res = new Response<string>();
            try
            {
                Console.WriteLine(Fail);
                Console.WriteLine(PayOrderId);
                Console.WriteLine(SignBy);
                if (Fail == "False")
                {
                    var result = await manageCallBackService.CallBackPromoteHealth(PayOrderId, SignBy);
                    return result;
                }
                else
                {
                    res.Result = null;
                    res.Status = 0;
                    res.Message = "Fail";
                    return res;

                }
            }
            catch (Exception ex)
            {
                res.Result = null;
                res.Status = 0;
                res.Message = ex.Message;
                return res;
            }
        }
    }
}
