using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Configs;
using sso.mms.helper.PortalModel;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using sso.mms.portal.ext.ViewModels;
using sso.mms.helper.Data;
using sso.mms.notification.ViewModel;

namespace sso.mms.portal.ext.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        public string? env = ConfigureCore.ConfigENV;
        private readonly PortalDbContext db;
        private readonly UploadFileService uploadFileService;
        public CertificateController(PortalDbContext portalDbContext, UploadFileService uploadFileService)
        {
            this.db = portalDbContext;
            this.uploadFileService = uploadFileService;
        }
        [HttpGet("GetCertificate/{hospitalMCode}")]
        public async Task<ActionResult<CertificateT>> GetCertificate(string hospitalMCode)
        {
           
            var query = await db.CertificateTs.Where(x => x.HospitalMCode == hospitalMCode).OrderByDescending(y => y.Id).FirstOrDefaultAsync();
            Console.WriteLine(query);
            if (query != null)
            {
                return query;
            }
            else
            {
                return Ok(new { status = false, message = "No data in database" });
            }
            
           
        }


        [HttpPost("AddCertificate")]
        public async Task<ActionResult<CertificateT>> addCertificate(CertificateT certificate)
        {
            try
            {
                CertificateT c = new CertificateT()
                {
                    UploadImagePath = certificate.UploadImagePath,
                    IsActive = true,
                    IsStatus = 1,
                    CreateBy = certificate.CreateBy,
                    UpdateDate = DateTime.Now,
                    UpdateBy = certificate.UpdateBy,
                    HospitalMCode = certificate.HospitalMCode
                };
                await db.CertificateTs.AddAsync(c);
                await db.SaveChangesAsync();

                return Ok(new { status = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<ResponseUpload> UploadCertificate([FromForm] List<IFormFile>? file)
        {
            string Uploadurl;
            Uploadurl = $"{ConfigureCore.redirectPortalExt}Files/";

            (string errorMessage, string UploadImagePath) = await uploadFileService.UploadImage(file,"");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                var fail = new ResponseUpload
                {
                    FileName = "",
                    Path_Url = "",
                    Error = errorMessage
                };
                return fail;
            }
            if (String.IsNullOrEmpty(UploadImagePath))
            {
                var fail = new ResponseUpload
                {
                    FileName = "",
                    Path_Url = "",
                    Error = "Cannot Upload"
                };
                return fail;
            }
            var success = new ResponseUpload
            {
                FileName = UploadImagePath,
                Path_Url = Uploadurl + UploadImagePath,
            };

            return success;
        }
    }


}