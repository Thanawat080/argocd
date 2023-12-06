using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;

namespace sso.mms.fees.api.Controllers.PromoteHealth.EXT
{
    [ApiController]
    [Route("api/[area]/[controller]")]
    [Area("PromoteHealth/Ext")]
    public class ManageWithdrawalController
    {
        private readonly IManageWithdrawalExtServices extService;
        
        public ManageWithdrawalController(IManageWithdrawalExtServices extService)
        {
            this.extService = extService;
        }

        [HttpGet("GetWithdrawalByHoscode/{hoscode}")]
        public async Task<List<WithdrawalView>> GetWithdrawalByHoscode(string hoscode)
        {
            List<WithdrawalView> result = await extService.GetWithdrawalByHoscode(hoscode);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetCheckupHInWithdrawal")]
        public async Task<List<ViewAaiHealthCheckupH>> GetCheckupHInWithdrawal(string hoscode, string withdrawalNo)
        {
            List<ViewAaiHealthCheckupH> result = await extService.GetCheckupHInWithdrawal(hoscode, withdrawalNo);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        //[HttpPost("UploadFile")]
        //public async Task<JsonResult> UploadFile([FromForm] List<IFormFile>? upload)
        //{
        //    string Uploadurl;

        //    //Keep develop and production separate.
        //    //Uploadurl = Path Directory 
        //    Uploadurl = $"{ConfigureCore.redirectFeesExt}Files/";
        //    var uploadFileService = new UploadFileService(null, null, null);
        //    (string errorMessage, string imageName) = await uploadFileService.UploadImage(upload, "");
        //    if (!String.IsNullOrEmpty(errorMessage))
        //    {
        //        return new JsonResult(new { Success = "False", responseText = errorMessage, error = new { message = "ขนาดไฟล์รูปภาพไม่เกิน 10 MB " } });
        //    }

        //    try
        //    {
        //        var success = new ResponseUpload
        //        {
        //            //Uploaded = 1,
        //            FileName = imageName,
        //            Path_Url = Uploadurl + imageName,
        //            Url = Uploadurl + imageName,
        //        };

        //        return new JsonResult(success);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { Success = "False", responseText = ex.Message });
        //    }
        //}

        [HttpPost("EditWithdrawalDoc")]
        //public async Task<string> EditWithdrawalDoc([FromForm] WithdrawalDocView dataDoc)
        public async Task<string> EditWithdrawalDoc([FromForm] WithdrawalDocView dataDoc)
        {
            var result = await extService.EditWithdrawalDoc(dataDoc);

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
