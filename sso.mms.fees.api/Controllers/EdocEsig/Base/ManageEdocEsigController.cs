using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Interface.EdocEsig.Base;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.ViewModels.EdocEsig;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;
namespace sso.mms.fees.api.Controllers.EdocEsig.Base
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("EdocEsig/base")]
    public class ManageEdocEsigController : ControllerBase
    {
        private readonly IManageEdocEsigServices manageEdocEsigServices;

        public ManageEdocEsigController(IManageEdocEsigServices manageEdocEsigServices)
        {
            this.manageEdocEsigServices = manageEdocEsigServices;
        }

        [HttpGet("GenReportFromDB")]
        public async Task<Response<string>> genReportFromDB(string apiUrl, string pathFile)
        {
            Response<string> res = new Response<string>();
            try
            {
                var result = await manageEdocEsigServices.GenReportFromDB(apiUrl,  pathFile);
                return result;
            }
            catch (Exception ex)
            {
                res.Result = null;
                res.Status = 0;
                res.Message = ex.Message;
                return res;
            }
        }

        [HttpPost("CreateDocument")]
        public async Task<Response<ResCreateDocument>> createDocument(CreateDocument data)
        {
            Response<ResCreateDocument> res = new Response<ResCreateDocument>();
            try
            {
                var result = await manageEdocEsigServices.CreateDocument(data);
                return result;
            }
            catch (Exception ex)
            {
                res.Result = null;
                res.Status = 0;
                res.Message = ex.Message;
                return res;
            }
        }


        [HttpPost("SendSign")]
        public async Task<Response<ResSendSign>> SendSign(SendSignView data)
        {
            Response<ResSendSign> res = new Response<ResSendSign>();
            try
            {
                var result = await manageEdocEsigServices.SendSign(data);
                return result;
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
