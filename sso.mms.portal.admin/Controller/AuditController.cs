using Blazorise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.Utility;
using sso.mms.helper.ViewModels;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.Email;
using sso.mms.portal.admin.Pages.EditBanner;
using sso.mms.portal.admin.ViewModels;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;

namespace sso.mms.portal.admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly IdpDbContext db;
        private readonly UploadFileService uploadFileService;
        public string? SecretKey = ConfigureCore.SecretKey;
        private readonly HttpClient httpClient;
        public AuditController(IdpDbContext db , UploadFileService uploadFileService, HttpClient httpClient)
        {
            this.db = db;
            this.uploadFileService = uploadFileService;
            this.httpClient = httpClient;
        }

        [HttpPost("AddAudit")]
        public async Task<IActionResult> AddAudit (AuditorUserM request)
        {
           
            try
            {
                AuditorUserM audit = new AuditorUserM()
                {
                    PrefixMCode=request.PrefixMCode,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    IdenficationNumber = request.IdenficationNumber,
                    CertNo = request.CertNo,
                    Position = request.Position,
                    StartDate = request.StartDate,
                    ExpireDate = request.ExpireDate,
                    Birthdate = request.Birthdate,
                    Email = request.Email,
                    Mobile = request.Mobile,
                    IsStatus = request.IsStatus,
                    IsActive = true,
                    UserName = request.UserName,
                    Password = request.Password,
                    ImagePath = request.ImagePath,
                    ImageName = request.ImageName,
                    CreateBy = request.CreateBy,
                    UpdateBy = request.UpdateBy,
                };
                await db.AuditorUserMs.AddAsync(audit);
                await db.SaveChangesAsync();

                // sync audit
                var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncAuditUser");
                Console.WriteLine(responseadduser);

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("DeleteAudit/{itemid}")]
        public async Task<ResponseModel> DeleteAudit(int itemid)
        {
           ResponseModel response = new ResponseModel();
            try
            {
                AuditorUserM auditorUserM = await db.AuditorUserMs.FirstOrDefaultAsync(x => x.Id == itemid);
                auditorUserM.IsActive = false;
                await db.SaveChangesAsync();
                response = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode ="200",
                    statusMessage = "Delete success",
                };
                return response;
            }
            catch(Exception ex)
            {
                response = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message,
                };
                return response;
            }
            
            
        }
                
        [HttpGet("GetAudit")]
        public async Task<IEnumerable<AuditorUserM>> GetAudit()
        {
            var query = await db.AuditorUserMs
            .OrderByDescending(e => e.Id).Where(w => w.IsActive == true).ToListAsync();
            
            return query;
        }


        [HttpPost("EditAudit")]
        public async Task<IActionResult> EditAudit(AuditorUserM request)
        {

            var result = await db.AuditorUserMs.Where(w => w.IsActive == true && w.Id == request.Id).FirstOrDefaultAsync();

            try
            {
               
                result.PrefixMCode = request.PrefixMCode;
                result.FirstName = request.FirstName;
                result.MiddleName = request.MiddleName;
                result.LastName = request.LastName;
                result.IdenficationNumber = request.IdenficationNumber;
                result.CertNo = request.CertNo;
                result.Position = request.Position;
                result.StartDate = request.StartDate;
                result.ExpireDate = request.ExpireDate;
                result.Birthdate = request.Birthdate;
                result.Email = request.Email;
                result.Mobile = request.Mobile;
                result.IsStatus = request.IsStatus;
                result.IsActive = true;
                result.ImagePath = request.ImagePath;
                result.ImageName = request.ImageName;

                db.AuditorUserMs.Update(result);
                await db.SaveChangesAsync();
                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("GetAuditById")]
        public async Task<ActionResult<AuditorUserM>> GetAuditById(int auditorId)
        {
            return await db.AuditorUserMs.Where(w => w.IsActive == true && w.Id == auditorId).FirstOrDefaultAsync();
        }

        [HttpPost("UploadFile")]
        public async Task<JsonResult> UploadFile([FromForm] List<IFormFile>? upload)
        {
            string Uploadurl;

            //Keep develop and production separate.
            //Uploadurl = Path Directory 
            Uploadurl = $"{ConfigureCore.redirectsPortalAdmin}Files/";

            (string errorMessage, string imageName) = await uploadFileService.UploadImage(upload,"");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                return new JsonResult(new { Success = "False", responseText = errorMessage });
            }

            try
            {
                var success = new ResponseUpload
                {
                    //Uploaded = 1,
                    FileName = imageName,
                    Path_Url = Uploadurl + imageName,
                    Url = Uploadurl + imageName,
                };

                return new JsonResult(success);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel value)
        {
            try
            {
                var encryptedString = AesOperation.EncryptString(SecretKey, value.newpassword);

                var userdata = await db.AuditorUserMs.FindAsync(value.userid);
                if (userdata != null)
                {
                    userdata.Id = value.userid;
                    userdata.UpdateDate = DateTime.Now;
                    userdata.UpdateBy = value.userid.ToString();
                    userdata.Password = encryptedString;
                    //userdata.Password = value.newpassword;
                }
                //idpDbContext.HospitalUserMs.Update(userdata);
                await db.SaveChangesAsync();

                return Ok(new { StatusCode = "true" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = "false", message = ex.Message });
            }
        }
    }
}
